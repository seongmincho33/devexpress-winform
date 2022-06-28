```C#
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PedasBase;
using PedasBase.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PEDAS_ConverterLib
{
    public partial class ConvertPDMSForm : Form
    {
        DataTable dtPedasObject;
        DataTable dtPart;
        DataTable dtSystemPath;
        Form mainForm;
        public Form SelfForm;

        List<PedasFileInfo> ConvertList = new List<PedasFileInfo>();
        DataTable dtFooting = new DataTable();
        DataTable dtPedestal = new DataTable();
        DataTable dtTieGirder = new DataTable();
        DataTable dtDummy = new DataTable();

        ArrayList xGridColInfo_FDNList = new ArrayList();
        ArrayList xGridColInfo_PEDList = new ArrayList();
        ArrayList xGridColInfo_Tiegirder = new ArrayList();
        ArrayList xGridColInfo_Dummy = new ArrayList();

        List<string> FootingPropertyName = new List<string>();
        List<string> FootingUserPropertyName = new List<string>();
        List<string> FootingRingPropertyName = new List<string>();
        List<string> PedestalPropertyName = new List<string>();
        List<string> PedRingPropertyName = new List<string>();
        List<string> TieGirderPropertyName = new List<string>();

        List<FdnInfo> targetFooting = new List<FdnInfo>();
        List<FdnPedInfo> targetPedestal = new List<FdnPedInfo>();
        List<TieGirderInfo> targetTieGirder = new List<TieGirderInfo>();
        List<DummyInfo> targetDummy = new List<DummyInfo>();

        private RevisionDataType FocusedRevisionType = RevisionDataType.UnChanged;
        private PedasFdnName SourcePedasType = PedasFdnName.Footing;

        DataTable dtSummary = new DataTable();
        DataTable dtCompare = new DataTable();

        bool ConvertAction = false;
        const int Step1Width = 865;
        const int Step2Width = 960;
        const int FormHeight = 590;
        Point NowPoint;

        List<RevisionItemInfo> PedasSystems;

        private bool isClose = true;
        private bool UseFdn = false, UsePile = false, UsePed = false, UseTie = false, UseSubBase = false;
        private ToolTip toolTip = new ToolTip();

        //PDMS
        private Timer timer;
        string XmlDataFolderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PEDAS_PDMS_DATA");
        string XmlDataFolderPath_FOUNDATION = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PEDAS_PDMS_DATA", "FOUNDATION");
        const string ConverterDataToPDMS = @"\ConverterDataToPDMS.xml";
        const string ErrorTextFileName = @"\PDMS_ERROR.xml";
        List<int> checkedDesignExplorer = new List<int>();

        private enum TableNameForPDMS
        {
            CONVERTER_FOUNDATIONS_DATA,
            CONVERTER_FOOTING_DATA,
            CONVERTER_SUBBASE_DATA,
            CONVERTER_HOLE_DATA,
            CONVERTER_HOLE_PEDESTAL_DATA,
            CONVERTER_PEDESTAL_DATA,
            CONVERTER_PILE_DATA,
            CONVERTER_TIEGIRDER_DATA
        }

        private bool OWN_FILE
        {
            get
            {
                return rgRevisionOption.SelectedIndex == 1;
            }
        }
        private bool DesignExplorerOption
        {
            get
            {
                //true 일때 전체 선택
                return rgDesignExplorerOption.SelectedIndex == 0;
            }
        }


        public ConvertPDMSForm(Form f, Form selfForm, DataTable dtPartData = null, DataTable dtSystemPath = null)
        {
            #region 폼 화면 셋팅
            InitializeComponent();
            this.Load += ConvertWizardForm_Load;
            this.Shown += this.ConvertWizardForm_Shown;
            this.FormClosing += this.ConvertWizardForm_FormClosing;
            this.FormClosed += this.ConvertWizardForm_FormClosed;
            #endregion

            SplashScreen13.Wait.Show(this, "Loading...");

            // Start Page                        
            this.mainForm = f;
            this.SelfForm = selfForm;
            this.dtPart = dtPartData;
            this.dtSystemPath = dtSystemPath;
            this.dtPedasObject = PedasData.ConvertObjectTable;

            #region 위저드 셋팅
            // 폰트 사이즈 통일
            //this.wizardControl1.Appearance.ExteriorPageTitle.Font = new Font(this.wizardControl1.Appearance.ExteriorPageTitle.Font.FontFamily, 9);            
            this.wizardControl1.Appearance.PageTitle.Font = new Font(this.wizardControl1.Appearance.ExteriorPageTitle.Font.FontFamily, 14, FontStyle.Bold);
            this.wizardControl1.FinishText = "Convert";
            this.wizardControl1.PrevClick += this.WizardControl1_PrevClick;
            this.wizardControl1.NextClick += this.WizardControl1_NextClick;
            this.wizardControl1.FinishClick += this.WizardControl1_FinishClick;
            this.wizardControl1.CancelClick += this.WizardControl1_CancelClick;
            this.wizardControl1.CustomizeCommandButtons += this.WizardControl1_CustomizeCommandButtons;
            #endregion

            #region 버튼 셋팅
            this.btnExcel.Click += this.BtnExcel_Click;
            this.btnBack.Click += this.BtnBack_Click;
            this.btnSetS3DCreateNode.Click += this.BtnSetS3DCreateNode_Click;
            this.pnlBack.Visible = false;
            #endregion

            #region tree list 셋팅
            this.tlObject.CustomDrawNodeCell += this.TlObject_CustomDrawNodeCell;
            this.tlObject.ShowingEditor += this.TlObject_ShowingEditor;
            this.tlObject.CellValueChanging += this.TlObject_CellValueChanging;
            this.tlSystemPath.CellValueChanging += this.TlSystemPath_CellValueChanging;
            this.tlSystemPath.CellValueChanged += this.TlSystemPath_CellValueChanged;
            this.tlSystemPath.StateImageList = Common.GetFoundationImageCollection();
            this.tlSystemPath.GetStateImage += this.TlSystemPath_GetStateImage;
            this.tlSystemPath.ShowingEditor += this.TlSystemPath_ShowingEditor;
            this.tlSystemPath.CustomDrawNodeCell += this.TlSystemPath_CustomDrawNodeCell;
            this.tlSystemPath.GetNodeDisplayValue += TlSystemPath_GetNodeDisplayValue;
            InitDataTotlSystmePath();
            #endregion

            InitConvertPageControl();
            InitRevisionPageControl();

            #region 컨텍스트 메뉴 초기화
            var contextMenuFdn = new ContextMenuStrip();
            var menu = new ToolStripMenuItem("Zoom to PEDAS-Converter");
            menu.Click += this.MenuFdn_Click;
            menu.Image = Properties.Resources.show_16x16;
            menu.Tag = "Eyeshot";
            contextMenuFdn.Items.Add(menu);
            menu = new ToolStripMenuItem("Zoom to PDMS");
            menu.Click += this.MenuFdn_Click;
            menu.Image = Properties.Resources.Smart3D_Icon;
            contextMenuFdn.Items.Add(menu);
            gcFoundation.ContextMenuStrip = contextMenuFdn;

            var contextMenuPed = new ContextMenuStrip();
            menu = new ToolStripMenuItem("Zoom to PEDAS-Converter");
            menu.Click += this.MenuPed_Click;
            menu.Image = Properties.Resources.show_16x16;
            menu.Tag = "Eyeshot";
            contextMenuPed.Items.Add(menu);
            menu = new ToolStripMenuItem("Zoom to PDMS");
            menu.Click += this.MenuPed_Click;
            menu.Image = Properties.Resources.Smart3D_Icon;
            contextMenuPed.Items.Add(menu);
            gcPedestal.ContextMenuStrip = contextMenuPed;

            var contextMenuTg = new ContextMenuStrip();
            menu = new ToolStripMenuItem("Zoom to PEDAS-Converter");
            menu.Click += this.MenuTie_Click;
            menu.Image = Properties.Resources.show_16x16;
            menu.Tag = "Eyeshot";
            contextMenuTg.Items.Add(menu);
            menu = new ToolStripMenuItem("Zoom to PDMS");
            menu.Click += this.MenuTie_Click;
            menu.Image = Properties.Resources.Smart3D_Icon;
            contextMenuTg.Items.Add(menu);
            gcTieGirder.ContextMenuStrip = contextMenuTg;
            #endregion        

            #region 툴팁 초기화
            SuperToolTip toolTip1 = new SuperToolTip();
            ToolTipTitleItem titleItem1 = new ToolTipTitleItem();
            titleItem1.Text = "Revision을 위한 옵션이며, 선택된 System 기준으로 PEDAS-Converter에서 추가된 Items을 가져온다.";
            ToolTipItem item1 = new ToolTipItem();
            item1.Text = @"* Foundation Hierarchy: 전체 Converter파일에서 추가된 Items을 가져옴.
* PEDAS-Converter File + Foundation Hierarchy: 현재의 Converter파일에서 추가된 Items을 가져옴.";
            toolTip1.Items.Add(titleItem1);
            toolTip1.Items.Add(item1);
            toolTip1.MaxWidth = 400;

            ToolTipController toolTip = new ToolTipController();
            toolTip.BeforeShow += this.ToolTip_BeforeShow;
            toolTip.AllowHtmlText = true;
            toolTip.ToolTipType = ToolTipType.SuperTip;
            toolTip.SetSuperTip(this.rgRevisionOption, toolTip1);
            #endregion

            #region PDMS 타이머
            this.timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();
            #endregion

            SplashScreen13.Wait.Close();
        }     

        #region 폼 로드 및 종료 이벤트
        private void ConvertWizardForm_Load(object sender, EventArgs e)
        {
            this.Location = this.Owner.Location;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(Step1Width, FormHeight); //폼 사이즈
            if (PedasData.ConvertProgramMode == ProgramMode.PDMS)
            {
                this.PDMS_ControlSetting();
            }
            else
            {
                this.rgDesignExplorerOption.Visible = false;
                this.panel14.Visible = false;
            }
        }

        private void PDMS_ControlSetting()
        {
            if (PedasData.ConvertProgramMode == ProgramMode.PDMS)
            {
                this.gcSelect.Text = "PDMS Element";
                this.btnSetS3DCreateNode.Visible = false;
                this.panel13.Visible = false;

                //라디오
                for (int i = 0; i < this.dtSystemPath.Rows.Count; i++)
                {
                    if ((bool)this.dtSystemPath.Rows[i]["Check"] == true)
                    {
                        this.checkedDesignExplorer.Add(i);
                    }
                }
                this.rgDesignExplorerOption.SelectedIndexChanged += RgDesignExplorerOption_SelectedIndexChanged;
                this.rgDesignExplorerOption.SelectedIndex = 1;
            }
        }

        private void ConvertWizardForm_Shown(object sender, EventArgs e)
        {
            // Finish 에서만 사용하지만, 속도 문제로 로드할때
            if (PedasData.RunType == ProgramRunType.Alone)
            {
                XtraMessageBox.Show(this, "Cannot Use PDMS data in Stand-Alone Mode!", Common.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.wizardPage1.AllowFinish = false;
            }
            //else
            //{
            //    s3dPath = HuenHelper.GetSystemPaths();
            //}
        }

        private void ConvertWizardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClose == false)
            {
                e.Cancel = true;
                isClose = true;
            }
            else
            {
                this.SelfForm = null;
                timer.Stop();
                timer.Tick -= Timer_Tick;
            }
        }

        private void ConvertWizardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Dispose();
            timer = null;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var registry_value = clsBasic.GetRegistryValue("PDMSCommand");
            var registry_value_error = clsBasic.GetRegistryValue("PDMS_ERROR");
            try
            {
                switch (registry_value)
                {
                    case nameof(CommandMessage.PDMS_DRAW_COMPLETE):
                        clsBasic.SetRegistryValue("PDMSCommand", nameof(CommandMessage.PDMS_COMMAND_WAITING));
                        SplashScreen13.Wait.Close();
                        MessageBox.Show("Converting to PDMS Completed!");
                        this.wizardPage1.AllowCancel = true;
                        break;
                    case nameof(CommandMessage.PDMS_REVISION_READY):
                        clsBasic.SetRegistryValue("PDMSCommand", nameof(CommandMessage.PDMS_COMMAND_WAITING));

                        #region 받아온 리비전 데이터 읽음.                
                        List<RevisionItemInfo> revisionItemInfoList = new List<RevisionItemInfo>();
                        DataSet dsRevision = new DataSet();
                        dsRevision.ReadXml(XmlDataFolderPath + @"\REVISION.xml");

                        if (dsRevision.Tables.Contains("REVISION_LIST"))
                        {
                            DataTable dtRevisionList = dsRevision.Tables["REVISION_LIST"];
                            DataTable dtPointList = dsRevision.Tables["POINT_LIST"];

                            foreach (DataRow drRevision in dtRevisionList.Rows)
                            {
                                string zone_refno = drRevision["ZONE_REFNO"].ToString();
                                string eqipment_refno = drRevision["EQUIPMENT_REFNO"].ToString();
                                string self_refno = drRevision["SELF_REFNO"].ToString();
                                string note = drRevision["NOTE"].ToString();
                                string note_dim = drRevision["NOTE_DIM"].ToString();
                                string convertkey = drRevision["CONVERTKEY"].ToString();
                                string foundationType = drRevision["FOUNDATION_TYPE"].ToString();
                                double height = this.StringToDouble(drRevision["HEIGHT"].ToString());
                                Guid PDMS_GROUP_ID = new Guid(drRevision["PDMS_GROUP_ID"].ToString());

                                FdnInfo fdn = new FdnInfo();
                                FdnPedInfo ped = new FdnPedInfo();
                                TieGirderInfo tie = new TieGirderInfo();
                                switch (foundationType)
                                {
                                    case "FOOTING":
                                        fdn = this.GetPDMSFoundation(note, note_dim, height, zone_refno, eqipment_refno, self_refno, dtRevisionList, dtPointList);
                                        revisionItemInfoList.Add(new RevisionItemInfo(note, convertkey, PedasFdnName.Footing, fdn, zone_refno, PDMS_GROUP_ID));
                                        break;
                                    case "PEDESTAL":
                                        ped = GetPDMSPedestal(note, note_dim, height, zone_refno, self_refno, dtRevisionList, dtPointList);
                                        revisionItemInfoList.Add(new RevisionItemInfo(note, convertkey, PedasFdnName.Pedestal, ped, zone_refno, PDMS_GROUP_ID));
                                        break;
                                    case "TIE_GIRDER":
                                        tie = GetPDMSTieGirder(note, note_dim, height, zone_refno, self_refno, dtRevisionList, dtPointList);
                                        revisionItemInfoList.Add(new RevisionItemInfo(note, convertkey, PedasFdnName.TieGirder, tie, zone_refno, PDMS_GROUP_ID));
                                        break;
                                }

                                //note file key 제거여부 판단후 제거
                                if (note != null && !this.OWN_FILE)
                                {
                                    string value = string.Empty;

                                    //if (S3D_Note.Split('◆').Length == 7)
                                    if (note.Length > 0)
                                    {
                                        // 맨앞의 File Key 를 제외한 그 이후 값
                                        for (int i = 1; i <= note.Split('◆').Length - 1; i++)
                                        {
                                            value += (note.Split('◆')[i] + "◆");
                                        }

                                        value = value.Substring(0, value.Length - 1);
                                    }

                                    note = value;
                                }
                            }
                        }

                        this.PedasSystems = revisionItemInfoList;
                        SetPedestalToFoundationInPedasSystem();

                        #endregion

                        #region PDMS Revision
                        //gc 바인딩 소스 초기화
                        gcFoundation.RefreshDataSource();
                        gcPedestal.RefreshDataSource();
                        gcTieGirder.RefreshDataSource();
                        gcDummy.RefreshDataSource();

                        // Name 다음에 위치 : 리비전 상태 컬럼
                        gvFoundation.Columns["RevisionType"].Visible = true;
                        gvFoundation.Columns["RevisionType"].VisibleIndex = 2;

                        gvPedestal.Columns["RevisionType"].Visible = true;
                        gvPedestal.Columns["RevisionType"].VisibleIndex = 2;

                        gvTieGirder.Columns["RevisionType"].Visible = true;
                        gvTieGirder.Columns["RevisionType"].VisibleIndex = 2;

                        gvDummy.Columns["RevisionType"].Visible = true;
                        gvDummy.Columns["RevisionType"].VisibleIndex = 2;

                        //현재 리비전 상태 셋팅
                        SetRevisionType_PDMS();
                        //리비전 서머리 셋팅
                        CalcSummary(this.SourcePedasType);

                        //텝 페이지에 객체 개수 보여줌
                        xtpFooting.Text = string.Format("Footing [{0}]", dtFooting.Rows.Count);
                        xtpPedestal.Text = string.Format("Pedestal [{0}]", dtPedestal.Rows.Count);
                        xtpTieGirder.Text = string.Format("Tie-Girder [{0}]", dtTieGirder.Rows.Count);
                        xtpDummy.Text = string.Format("Dummy [{0}]", dtDummy.Rows.Count);
                        #endregion

                        //리비전 가져오고 버튼 클릭할 수 있게 함.
                        this.wizardPage1.AllowBack = true;
                        this.wizardPage1.AllowFinish = true;

                        SplashScreen13.Wait.Close();
                        break;
                }
                if (registry_value_error == CommandMessage.PDMS_ERROR_SEND.ToString())
                {
                    //PDMS 의 오류 알림기능
                    string XmlDataFolderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PEDAS_PDMS_DATA");
                    string error_text = System.IO.File.ReadAllText(XmlDataFolderPath + @"\PDMS_ERROR.xml", System.Text.Encoding.Default);
                    clsBasic.SetRegistryValue("PDMS_ERROR", CommandMessage.CONVERTER_ERROR_RECEIVED.ToString());
                    DevExpress.XtraEditors.XtraMessageBox.Show(error_text, "[PDMS ERROR]");
                }
            }
            catch (Exception ex)
            {
                SplashScreen13.Wait.Close();
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.ToString(), "Wizard Timer 오류");
            }
        }

        #region PDMS로부터 PedasSystem        
        private void SetPedestalToFoundationInPedasSystem()
        {
            foreach (RevisionItemInfo footing in this.PedasSystems)
            {
                if (footing.FoundationType == PedasFdnName.Footing)
                {
                    ((FdnInfo)footing.GraphicItem).PedestalList_Info = new List<FdnPedInfo>();
                    foreach (RevisionItemInfo pedestal in this.PedasSystems)
                    {
                        if (pedestal.FoundationType == PedasFdnName.Pedestal
                            && pedestal.PDMS_GROUP_ID == footing.PDMS_GROUP_ID)
                        {
                            ((FdnInfo)footing.GraphicItem).PedestalList_Info.Add(((FdnPedInfo)pedestal.GraphicItem));
                        }
                    }
                }
            }
        }

        private FdnInfo GetPDMSFoundation(string note, string note_dim, double height, string zone_refno, string equipment_refno, string self_refno, DataTable dtRevisionList, DataTable dtPointList)
        {
            FdnInfo fdn = new FdnInfo();

            fdn.SystemID = zone_refno;

            var points = GetPDMSPoints(note, dtPointList, self_refno, "FOOTING");

            fdn.IsS3D = true;
            fdn.S3D_Note = note;
            // FILE_ID◆PROJECT◆UNIT◆MODEL◆EQUIP◆PED_NAMES◆FDN_NAME
            string[] arr = note.Split('◆');
            points.ForEach(x => x.FILE_ID = arr[0]);
            fdn.FdnPointList = points;
            fdn.FDN_Name = arr[arr.Length - 1];
            fdn.isPileFdn = false;
            fdn.GlobalZ = Math.Round(points.Max(x => x.Z), 3);

            fdn.Hf_mm = height;

            GetDimValue(PedasFdnName.Footing, note_dim, fdn, null, null);

            fdn.Lx_mm = fdn.lx_dim;
            fdn.Ly_mm = fdn.ly_dim;

            var subBases = dtRevisionList
                        .AsEnumerable()
                        .Where(row =>
                        row["NOTE"].ToString() == note
                        && row["FOUNDATION_TYPE"].ToString().Contains("SUB"));
            foreach (var subBase in subBases)
            {
                if (subBase["FOUNDATION_TYPE"].ToString() == "SUB_Screed")
                {
                    fdn.ScreedThk = this.StringToDouble(subBase["HEIGHT"].ToString()) / 1000;
                }
                else if (subBase["FOUNDATION_TYPE"].ToString() == "SUB_Lean")
                {
                    fdn.LeanConcrete = this.StringToDouble(subBase["HEIGHT"].ToString()) / 1000;
                }
                else if (subBase["FOUNDATION_TYPE"].ToString() == "SUB_Crushed")
                {
                    fdn.CrushedStones = this.StringToDouble(subBase["HEIGHT"].ToString()) / 1000;
                }
            }

            this.GetPDMSPile(note, equipment_refno, dtRevisionList, dtPointList, fdn);

            fdn.FdnPointList.AddRange(this.GetPDMSHolePoints(note, dtPointList, self_refno, "FOOTING_HOLE"));
            return fdn;
        }

        private FdnPedInfo GetPDMSPedestal(string note, string note_dim, double height, string zone_refno, string self_refno, DataTable dtRevisionList, DataTable dtPointList)
        {
            FdnPedInfo ped = new FdnPedInfo();

            ped.SystemID = zone_refno;

            var points = GetPDMSPoints(note, dtPointList, self_refno, "PEDESTAL");

            ped.IsS3D = true;
            ped.S3D_Note = note;
            // FILE_ID◆PROJECT◆UNIT◆MODEL◆EQUIP◆PED_NAMES◆FDN_NAME
            string[] arr = note.Split('◆');
            ped.PED_NAME = arr[arr.Length - 1];
            points.ForEach(x => x.FILE_ID = arr[0]);
            ped.FdnPedPointList = points;
            ped.GlobalZ = Math.Round(points.Max(x => x.Z), 3);

            var grout = dtRevisionList
                        .AsEnumerable()
                        .Where(row =>
                        row["NOTE"].ToString() == note
                        && row["FOUNDATION_TYPE"].ToString() == "GROUT")
                        .FirstOrDefault();

            ped.Hp = height + this.StringToDouble(grout["HEIGHT"].ToString());
            ped.GlobalZ += this.StringToDouble(grout["HEIGHT"].ToString()) / 1000;

            GetDimValue(PedasFdnName.Pedestal, note_dim, null, ped, null);

            ped.dx = ped.dx_dim;
            ped.dy = ped.dy_dim;

            ped.FdnPedPointList.AddRange(this.GetPDMSHolePoints(note, dtPointList, self_refno, "PEDESTAL_HOLE"));

            return ped;
        }

        private TieGirderInfo GetPDMSTieGirder(string note, string note_dim, double height, string zone_refno, string self_refno, DataTable dtRevisionList, DataTable dtPointList)
        {
            TieGirderInfo tie = new TieGirderInfo();
            tie.IsS3D = true;

            tie.SystemID = zone_refno;

            var points = GetPDMSPoints(note, dtPointList, self_refno, "TIE_GIRDER");
            List<TieGirderPointInfo> tiePoints = new List<TieGirderPointInfo>();
            foreach (var sp in points)
            {
                // ID 와 NUM 은 의미 없음
                tiePoints.Add(new TieGirderPointInfo(0, sp.X, sp.Y, sp.Z));
            }

            tie.TGPoints = tiePoints;

            // FILE_ID◆PROJECT◆UNIT◆MODEL◆EQUIP◆PED_NAMES◆FDN_NAME
            string[] arr = note.Split('◆');
            tie.Name = arr[arr.Length - 1];

            tie.S3D_Note = note;

            tie.Height = height / 1000;

            GetDimValue(PedasFdnName.TieGirder, note_dim, null, null, tie);

            return tie;
        }

        private void GetPDMSPile(string fdn_note, string equipment_refno, DataTable dtRevision, DataTable dtPointList, FdnInfo fdn)
        {
            var piles = dtRevision
              .AsEnumerable()
              .Where(row => row["FOUNDATION_TYPE"].ToString() == "PILE"
                  && trimToNote(row["NOTE"].ToString()) == fdn_note
                  && row["EQUIPMENT_REFNO"].ToString() == equipment_refno
                  )
              .ToList();

            if (piles.Count == 0) return;

            fdn.isPileFdn = true;
            fdn.Pile_Ea = piles.Count;            

            foreach (var el in piles)
            {
                string[] arr = el["NOTE"].ToString().Split('◆');
                fdn.Pile_Shape = arr[8]; // "Rectangle", "Circle"
                fdn.Pile_Diameter = arr[9].ExToDouble();
                fdn.PilePrefix = arr[10];
                fdn.PileNo = arr[11].ExToInt();

                var points = GetPDMSPoints(el["NOTE"].ToString(), dtPointList, equipment_refno, "PILE");

                points.ForEach(x => x.FILE_ID = arr[0]);

                fdn.PileLength_mm = this.StringToDouble(el["HEIGHT"].ToString());

                break;
            }

            string trimToNote(string str)
            {
                string value = string.Empty;

                if (str.Split('◆').Length > 0)
                {
                    for (int i = 0; i <= str.Split('◆').Length - 6; i++)
                    {
                        value += (str.Split('◆')[i] + "◆");
                    }

                    value = value.Substring(0, value.Length - 1);
                }

                return value;
            }
        }

        private List<FdnPoint> GetPDMSPoints(string note, DataTable dt, string owner_extrusion_refno, string foundationType = null)
        {
            var points = new List<FdnPoint>();
            EnumerableRowCollection<DataRow> PDMS_Points = null;
            if (foundationType == null)
            {
                PDMS_Points = dt.AsEnumerable().Where(row => row["NOTE"].ToString() == note);
            }
            else if (foundationType != null)
            {
                PDMS_Points = dt.AsEnumerable().Where(row => row["NOTE"].ToString() == note
                && row["FOUNDATION_TYPE"].ToString() == foundationType
                && row["OWNER_EXTRUSION"].ToString() == owner_extrusion_refno);
            }
            foreach (DataRow drPoints in PDMS_Points)
            {
                FdnPoint f = new FdnPoint();
                f.X = this.StringToDouble(drPoints["XPOINT"].ToString());
                f.Y = this.StringToDouble(drPoints["YPOINT"].ToString());
                f.Z = this.StringToDouble(drPoints["ZPOINT"].ToString());

                if (points.Contains(f) == false)
                {
                    points.Add(f);
                }
            }
            return points;
        }

        private List<FdnPoint> GetPDMSHolePoints(string note, DataTable dt, string owner_extrusion_refno, string foundationType = null)
        {
            var points = new List<FdnPoint>();
            EnumerableRowCollection<DataRow> PDMS_Points = null;
            if (foundationType == null)
            {
                PDMS_Points = dt.AsEnumerable().Where(row => row["NOTE"].ToString() == note);
            }
            else if (foundationType != null)
            {
                PDMS_Points = dt.AsEnumerable().Where(row => row["NOTE"].ToString() == note
                && row["FOUNDATION_TYPE"].ToString() == foundationType
                && row["OWNER_EXTRUSION"].ToString() == owner_extrusion_refno);
            }
            foreach (DataRow drPoints in PDMS_Points)
            {
                FdnPoint f = new FdnPoint();
                f.X = this.StringToDouble(drPoints["XPOINT"].ToString());
                f.Y = this.StringToDouble(drPoints["YPOINT"].ToString());
                f.Z = this.StringToDouble(drPoints["ZPOINT"].ToString());

                if (points.Contains(f) == false)
                {
                    points.Add(f);
                }
            }
            return points;
        }

        private static void GetDimValue(PedasFdnName name, string note_dim, FdnInfo fdn, FdnPedInfo ped, TieGirderInfo tie)
        {
            string[] arr = note_dim.Split('|');

            if (name == PedasFdnName.Footing && fdn != null)
            {
                // return $"{FootShape}|{ScreedThk}|{LeanConcrete}|{CrushedStones}|{Lx_mm}|{Ly_mm}|{GlobalX}|{GlobalY}|{GlobalZ}";
                for (int i = 0; i < arr.Length; i++)
                {
                    double.TryParse(arr[i], out double val);

                    if (i == 0)
                        fdn.FootShape = PedasData.GetFootShape(arr[i]);
                    //else if (i == 1)
                    //    fdn.ScreedThk = val;
                    //else if (i == 2)
                    //    fdn.LeanConcrete = val;
                    //else if (i == 3)
                    //    fdn.CrushedStones = val;
                    else if (i == 4)
                        fdn.lx_dim = val;
                    else if (i == 5)
                        fdn.ly_dim = val;
                    else if (i == 6)
                        fdn.GlobalX = val;
                    else if (i == 7)
                        fdn.GlobalY = val;
                    //else if (i == 8)
                    //    fdn.GlobalZ = val;
                }
            }
            else if (name == PedasFdnName.Pedestal && ped != null)
            {
                //return $"{Shape}|{dx_dim}|{dy_dim}|{Hpa}";
                for (int i = 0; i < arr.Length; i++)
                {
                    double.TryParse(arr[i], out double val);

                    if (i == 0)
                        ped.Shape = PedasData.GetFootShape(arr[i]);
                    else if (i == 1)
                        ped.dx_dim = val;
                    else if (i == 2)
                        ped.dy_dim = val;
                    else if (i == 3)
                        ped.Hpa = val;
                    else if (i == 4)
                        ped.GlobalX = val;
                    else if (i == 5)
                        ped.GlobalY = val;
                    //else if (i == 6)
                    //    ped.GlobalZ = val;
                }
            }
            else if (name == PedasFdnName.TieGirder && tie != null)
            {
                // return $"{Width}";
                for (int i = 0; i < arr.Length; i++)
                {
                    double.TryParse(arr[i], out double val);

                    if (i == 0)
                        tie.Width = val;
                }
            }
        }

        private double StringToDouble(string str)
        {
            Double d = 0.0d;
            try
            {
                if (Double.TryParse(str, out d))
                {
                    return d;
                }
                else if (str == "0" || str == "" || str == null)
                {
                    return d;
                }
                else
                {
                    MessageBox.Show("문자 -> double 형변환 실패");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return d;
        }
        #endregion

        #endregion

        #region 버튼 이벤트
        private void WizardControl1_PrevClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.Size = new Size(Step1Width, FormHeight);
            InitConvertPageControl();

            this.Cursor = Cursors.Default;
        }

        private void WizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.Size = new Size(Step2Width, FormHeight);
            this.btnExcel.Visible = true;

            dtFooting.Clear();
            dtPedestal.Clear();
            dtTieGirder.Clear();
            dtDummy.Clear();

            //사용자가 선택한 객체만 변환 -> 체크된 타입만 AddTableRow
            GetSelectPedasObject();

            // System Check 되어있는 것 중에서 Revision                     
            this.ConvertList.Clear();

            foreach (var info in PedasData.GlobalPedasInfos)
            {
                if (info.Activated == false) continue;

                PedasFileInfo item = new PedasFileInfo();
                item.FILE_ID = info.FILE_ID;
                item.GlobalPosition = info.GlobalPosition;
                item.Activated = true;
                item.InfoOption = info.InfoOption;

                AddTableRow(info.FDN_LISTS, info.FDNPED_LISTS, info.TieGirder_LISTS, info.DummyList);

                this.ConvertList.Add(item);
            }

            //리비전 시작
            this.InitGridRevision();

            //gc 바인딩 소스 초기화
            this.gcFoundation.RefreshDataSource();
            this.gcPedestal.RefreshDataSource();
            this.gcTieGirder.RefreshDataSource();
            this.gcDummy.RefreshDataSource();

            #region 전체 선택
            int index = 0;
            for (; ; )
            {
                DataRow focusrow = gvFoundation.GetDataRow(index);

                if (focusrow == null)
                    break;

                //if(focusrow["RevisionType"].ExToString() != "UnChanged")
                //{
                gvFoundation.SelectRow(index);
                //}

                index++;
            }

            index = 0;
            for (; ; )
            {
                DataRow focusrow = gvPedestal.GetDataRow(index);

                if (focusrow == null)
                    break;

                //if (focusrow["RevisionType"].ExToString() != "UnChanged")
                //{
                gvPedestal.SelectRow(index);
                //}

                index++;
            }

            index = 0;
            for (; ; )
            {
                DataRow focusrow = gvTieGirder.GetDataRow(index);

                if (focusrow == null)
                    break;

                //if (focusrow["RevisionType"].ExToString() != "UnChanged")
                //{
                gvTieGirder.SelectRow(index);
                //}

                index++;
            }

            #endregion

            this.Cursor = Cursors.Default;

            if (SearchDuplicatedData())
            {
                //중복데이터 있음.
                XtraMessageBox.Show(this, "Please selected data.", Common.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.wizardPage1.AllowFinish = false;
            }

            //dtFooting
        }

        private bool SearchDuplicatedData()
        {
            List<DataRow> search = new List<DataRow>();
            foreach (DataRow tmp in dtFooting.Rows)
            {
                var FindList = dtFooting.AsEnumerable()
                    .Where(p => p["NoteWithoutFileKey"].ExToString() == tmp["NoteWithoutFileKey"].ExToString()
                    && p["SystemName"].ExToString() == tmp["SystemName"].ExToString())
                    .ToList();

                if (FindList.Count > 1)
                {
                    if (!search.Contains(FindList.First()))
                        search.Add(FindList.First());
                }
            }

            foreach (DataRow tmp in dtTieGirder.Rows)
            {
                var FindList = dtTieGirder.AsEnumerable().Where(p => p["NoteWithoutFileKey"].ExToString() == tmp["NoteWithoutFileKey"].ExToString() && p["SystemName"].ExToString() == tmp["SystemName"].ExToString()).ToList();

                if (FindList.Count > 1)
                {
                    if (!search.Contains(FindList.First()))
                        search.Add(FindList.First());
                }
            }

            foreach (DataRow tmp in dtDummy.Rows)
            {
                var FindList = dtDummy.AsEnumerable().Where(p => p["NoteWithoutFileKey"].ExToString() == tmp["NoteWithoutFileKey"].ExToString() && p["SystemName"].ExToString() == tmp["SystemName"].ExToString()).ToList();

                if (FindList.Count > 1)
                {
                    if (!search.Contains(FindList.First()))
                        search.Add(FindList.First());
                }
            }

            //중복데이터 존재시 true 반환
            if (search.Count > 0)
            {
                return true;
            }

            return false;
        }

        public void GetSelectPedasObject()
        {
            //사용자가 선택한 객체만 변환

            var row = dtPedasObject.AsEnumerable().FirstOrDefault(x => x["Type"].ToString() == PedasFdnName.Footing.ToString());
            if (row != null) UseFdn = Convert.ToBoolean(row["CHK"]);

            row = dtPedasObject.AsEnumerable().FirstOrDefault(x => x["NAME"].ToString() == PedasFdnName.SubBase.ToString());
            if (row != null) UseSubBase = Convert.ToBoolean(row["CHK"]);

            row = dtPedasObject.AsEnumerable().FirstOrDefault(x => x["NAME"].ToString() == PedasFdnName.Pile.ToString());
            if (row != null) UsePile = Convert.ToBoolean(row["CHK"]);

            row = dtPedasObject.AsEnumerable().FirstOrDefault(x => x["Type"].ToString() == PedasFdnName.Pedestal.ToString());
            if (row != null) UsePed = Convert.ToBoolean(row["CHK"]);

            row = dtPedasObject.AsEnumerable().FirstOrDefault(x => x["Type"].ToString() == PedasFdnName.TieGirder.ToString());
            if (row != null) UseTie = Convert.ToBoolean(row["CHK"]);

            //if(PedasData.ConvertProgramMode == ProgramMode.PDMS)
            //{
            //    //사용자가 선택한 Pedas Object만 비교함
            //    InitComparePropertyName();
            //    if (!UseFdn)
            //    {
            //        this.FootingUserPropertyName.Remove("FDN_Name");
            //        this.FootingUserPropertyName.Remove("isPileFdn");
            //        this.FootingUserPropertyName.Remove("FootShape");
            //        this.FootingUserPropertyName.Remove("Hf_mm");
            //        this.FootingUserPropertyName.Remove("isPileFdn");
            //        this.FootingUserPropertyName.Remove("Pile Point Group");
            //        this.FootingUserPropertyName.Remove("Pile_Diameter");
            //        this.FootingUserPropertyName.Remove("Pile_Ea");
            //        this.FootingUserPropertyName.Remove("Pile_Shape");
            //        this.FootingUserPropertyName.Remove("PileLength_mm");
            //        this.FootingUserPropertyName.Remove("PileNo");
            //        this.FootingUserPropertyName.Remove("PilePrefix");
            //        this.FootingUserPropertyName.Remove("PointGroup");
            //        this.FootingUserPropertyName.Remove("SystemChildPath");
                    
            //        this.FootingPropertyName.Remove("FDN_Name");
            //        this.FootingPropertyName.Remove("FootShape");
            //        this.FootingPropertyName.Remove("GlobalX");
            //        this.FootingPropertyName.Remove("GlobalY");
            //        this.FootingPropertyName.Remove("GlobalZ");
            //        this.FootingPropertyName.Remove("Hf_mm");
            //        this.FootingPropertyName.Remove("isPileFdn");
            //        this.FootingPropertyName.Remove("Lx_mm");
            //        this.FootingPropertyName.Remove("Ly_mm");
            //        this.FootingPropertyName.Remove("Pile Point Group");
            //        this.FootingPropertyName.Remove("Pile_Diameter");
            //        this.FootingPropertyName.Remove("Pile_Ea");
            //        this.FootingPropertyName.Remove("Pile_Shape");
            //        this.FootingPropertyName.Remove("PileLength_mm");
            //        this.FootingPropertyName.Remove("PileNo");
            //        this.FootingPropertyName.Remove("PilePrefix");
            //        this.FootingPropertyName.Remove("SystemChildPath");                    

            //        this.FootingRingPropertyName.Remove("FDN_NAME");
            //        this.FootingRingPropertyName.Remove("Lx_mm");
            //        this.FootingRingPropertyName.Remove("PileNo");
            //        this.FootingRingPropertyName.Remove("PilePrefix");
            //        this.FootingRingPropertyName.Remove("SystemChildPath");
            //    }
            //    if (!UseSubBase)
            //    {
            //        this.FootingUserPropertyName.Remove("ScreedThk");
            //        this.FootingUserPropertyName.Remove("LeanConcrete");
            //        this.FootingUserPropertyName.Remove("CrushedStones");

            //        this.FootingPropertyName.Remove("ScreedThk");
            //        this.FootingPropertyName.Remove("LeanConcrete");
            //        this.FootingPropertyName.Remove("CrushedStones");
            //    }
            //    if (!UsePile)
            //    {

            //    }
            //    if (!UsePed)
            //    {
            //        this.PedestalPropertyName.Remove("dx");
            //        this.PedestalPropertyName.Remove("dy");
            //        this.PedestalPropertyName.Remove("GlobalX");
            //        this.PedestalPropertyName.Remove("GlobalY");
            //        this.PedestalPropertyName.Remove("GlobalZ");
            //        this.PedestalPropertyName.Remove("Hp");
            //        this.PedestalPropertyName.Remove("PED_NAME");
            //        this.PedestalPropertyName.Remove("Shape");
            //        this.PedestalPropertyName.Remove("SystemChildPath");

            //        this.PedRingPropertyName.Remove("dx");
            //        this.PedRingPropertyName.Remove("PED_NAME");
            //        this.PedRingPropertyName.Remove("SystemChildPath");                    
            //    }
            //    if (!UseTie)
            //    {
            //        this.TieGirderPropertyName.Remove("");
            //        this.TieGirderPropertyName.Remove("");
            //        this.TieGirderPropertyName.Remove("");

            //    }
            //}           
        }

        private void AddTableRow(List<FdnInfo> fdnInfos, List<FdnPedInfo> fdnPedInfos, List<TieGirderInfo> tieGirderInfos, List<DummyInfo> dummyInfos)
        {
            //Path 매핑 된것만 테이블에 데이터로 추가함.
            if (UseFdn)
            {
                foreach (var fdn in fdnInfos)
                {
                    if (string.IsNullOrEmpty(fdn.SystemID)) continue;

                    if (dtSystemPath.Select(string.Format("ID='{0}' and Check = true", fdn.SystemID)).Length > 0)
                    {
                        DataRow row = dtFooting.NewRow();

                        row["FDN_ID"] = fdn.FDN_ID;
                        row["FDN_Name"] = fdn.FDN_Name;
                        row["FootShape"] = fdn.FootShape;
                        row["GlobalX"] = fdn.GlobalX;
                        row["GlobalY"] = fdn.GlobalY;
                        row["GlobalZ"] = fdn.GlobalZ;
                        row["Hf_mm"] = fdn.Hf_mm;
                        row["Hs_mm"] = fdn.Hs_mm;
                        row["Lx_mm"] = fdn.Lx_mm;
                        row["Ly_mm"] = fdn.Ly_mm;
                        row["Pile_Ea"] = fdn.Pile_Ea;
                        row["S3D_Note"] = fdn.S3D_Note;
                        row["RevisionType"] = fdn.RevisionType.ToString();
                        row["ScreedThk"] = fdn.ScreedThk;
                        row["LeanConcrete"] = fdn.LeanConcrete;
                        row["CrushedStones"] = fdn.CrushedStones;
                        row["LeanConcEdge"] = fdn.LeanConcEdge;
                        row["PilePrefix"] = fdn.PilePrefix;
                        row["PileNo"] = fdn.PileNo;
                        row["FDNTYPE_ID"] = fdn.FDNTYPE_ID;
                        row["FILE_ID"] = fdn.FILE_ID;
                        row["SystemName"] = fdn.SystemName;
                        row["NoteWithoutFileKey"] = fdn.NoteWithoutFileKey;
                        row["Entity"] = fdn;

                        dtFooting.Rows.Add(row);
                    }
                }
            }

            if (UsePed)
            {
                foreach (var ped in fdnPedInfos)
                {
                    if (string.IsNullOrEmpty(ped.FootingInfo.SystemID)) continue;

                    if (dtSystemPath.Select(string.Format("ID='{0}' and Check = true", ped.FootingInfo.SystemID)).Length > 0)
                    {
                        DataRow row = dtPedestal.NewRow();

                        row["PED_ID"] = ped.PED_ID;
                        row["PED_NAME"] = ped.PED_NAME;
                        row["Shape"] = ped.Shape;
                        row["GlobalX"] = ped.GlobalX;
                        row["GlobalY"] = ped.GlobalY;
                        row["GlobalZ"] = ped.GlobalZ;
                        row["Hp"] = ped.Hp;
                        row["Hpa"] = ped.Hpa;
                        row["dx"] = ped.dx;
                        row["dy"] = ped.dy;
                        row["Grout_Thk"] = ped.Grout_Thk;
                        row["S3D_Note"] = ped.S3D_Note;
                        row["RevisionType"] = ped.RevisionType.ToString();
                        row["PEDType_ID"] = ped.PEDType_ID;
                        row["FILE_ID"] = ped.FILE_ID;
                        row["SystemName"] = ped.SystemName;
                        row["NoteWithoutFileKey"] = ped.NoteWithoutFileKey;
                        row["Entity"] = ped;

                        dtPedestal.Rows.Add(row);
                    }
                }
            }

            if (UseTie)
            {
                foreach (var tie in tieGirderInfos)
                {
                    if (string.IsNullOrEmpty(tie.SystemID)) continue;

                    if (dtSystemPath.Select(string.Format("ID='{0}' and Check = true", tie.SystemID)).Length > 0)
                    {
                        DataRow row = dtTieGirder.NewRow();

                        row["OBJECTID"] = tie.OBJECTID;
                        row["Name"] = tie.Name;
                        row["Start_Ped_Name"] = tie.Start_Ped_Name;
                        row["End_Ped_Name"] = tie.End_Ped_Name;
                        row["Width"] = tie.Width;
                        row["Height"] = tie.Height;
                        row["StartPointX"] = tie.StartPointX;
                        row["StartPointY"] = tie.StartPointY;
                        row["StartPointZ"] = tie.StartPointZ;
                        row["EndPointX"] = tie.EndPointX;
                        row["EndPointY"] = tie.EndPointY;
                        row["StartPointZ"] = tie.StartPointZ;
                        row["EndPointZ"] = tie.EndPointZ;
                        row["S3D_Note"] = tie.S3D_Note;
                        row["RevisionType"] = tie.RevisionType.ToString();
                        row["PARENTID"] = tie.PARENTID;
                        row["FILE_ID"] = tie.FILE_ID;
                        row["SystemName"] = tie.SystemName;
                        row["NoteWithoutFileKey"] = tie.NoteWithoutFileKey;
                        row["Entity"] = tie;

                        dtTieGirder.Rows.Add(row);
                    }
                }
            }

            foreach (var dummy in dummyInfos)
            {
                if (string.IsNullOrEmpty(dummy.SystemID)) continue;

                if (dtSystemPath.Select(string.Format("ID='{0}' and Check = true", dummy.SystemID)).Length > 0)
                {
                    DataRow row = dtDummy.NewRow();

                    row["OBJECTID"] = dummy.OBJECTID;
                    row["DummyName"] = dummy.DummyName;
                    row["S3D_Note"] = dummy.S3D_Note;
                    row["RevisionType"] = dummy.RevisionType.ToString();
                    row["FILE_ID"] = dummy.FILE_ID;
                    row["SystemName"] = dummy.SystemName;
                    row["NoteWithoutFileKey"] = dummy.NoteWithoutFileKey;

                    row["Shape"] = dummy.Shape;
                    row["GlobalX"] = dummy.GlobalX;
                    row["GlobalY"] = dummy.GlobalY;
                    row["GlobalZ"] = dummy.GlobalZ;
                    row["PedWidth"] = dummy.PedWidth;
                    row["PedHeight"] = dummy.PedHeight;
                    row["PedLength"] = dummy.PedLength;
                    row["FootingWidth"] = dummy.FootingWidth;
                    row["FootingHeight"] = dummy.FootingHeight;
                    row["FootingLength"] = dummy.FootingLength;
                    row["Rotation"] = dummy.Rotation;
                    row["Remark"] = dummy.REMARK;

                    row["Entity"] = dummy;

                    dtDummy.Rows.Add(row);
                }
            }
        }

        private void WizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            if (gvFoundation.GetSelectedRows().Length == 0 && gvPedestal.GetSelectedRows().Length == 0 && gvTieGirder.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show(this, "Please selected data.", Common.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                isClose = false;
                return;
            }

            // Unchanged 의 Property 설정 옵션으로 인해서 pass
            //if (dtFooting.Select(string.Format("RevisionType='{0}' or RevisionType='{1}' or RevisionType='{2}'", RevisionDataType.Added.ToString(), RevisionDataType.Modified.ToString(), RevisionDataType.Deleted.ToString())).Length == 0
            //    && dtPedestal.Select(string.Format("RevisionType='{0}' or RevisionType='{1}' or RevisionType='{2}'", RevisionDataType.Added.ToString(), RevisionDataType.Modified.ToString(), RevisionDataType.Deleted.ToString())).Length == 0
            //    && dtTieGirder.Select(string.Format("RevisionType='{0}' or RevisionType='{1}' or RevisionType='{2}'", RevisionDataType.Added.ToString(), RevisionDataType.Modified.ToString(), RevisionDataType.Deleted.ToString())).Length == 0
            //    && dtDummy.Select(string.Format("RevisionType='{0}' or RevisionType='{1}' or RevisionType='{2}'", RevisionDataType.Added.ToString(), RevisionDataType.Modified.ToString(), RevisionDataType.Deleted.ToString())).Length == 0)
            //{
            //    XtraMessageBox.Show(this, "There is no convert data.", Common.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    e.Cancel = true;
            //    isClose = false;
            //    return;
            //}

            //화면 안보이게 함.
            this.Visible = false;
            mainForm.Visible = false;

            this.Cursor = Cursors.WaitCursor;

            // 기존에 추가되어있는 데이터
            PedasFileInfo targetItem = new PedasFileInfo();
            targetItem.FDN_LISTS = targetFooting;
            targetItem.FDNPED_LISTS = targetPedestal;
            targetItem.TieGirder_LISTS = targetTieGirder;
            targetItem.DummyList = targetDummy;

            // Converting 할 때
            //foreach (var fdn in targetItem.FDN_LISTS)
            //{
            //    if (string.IsNullOrEmpty(fdn.SystemID)) continue;
            //    else
            //    {
            //        if (fdn.System == null)
            //        {
            //            var tmp = s3dPath.FirstOrDefault(x => x.ID == fdn.SystemID);
            //            if (tmp != null) fdn.System = tmp.S3DSystem;
            //            else continue;
            //        }
            //    }
            //}

            foreach (var item in this.ConvertList)
            {
                var flist = new List<FdnInfo>();
                var plist = new List<FdnPedInfo>();
                var tglist = new List<TieGirderInfo>();

                /*
                foreach (DataRow row in dtFooting.Select(string.Format("FILE_ID='{0}'", item.FILE_ID)))
                {
                    FdnInfo fdn = row["Entity"] as FdnInfo;
                    flist.Add(fdn);
                }
                foreach (DataRow row in dtPedestal.Select(string.Format("FILE_ID='{0}'", item.FILE_ID)))
                {
                    FdnPedInfo ped = row["Entity"] as FdnPedInfo;
                    plist.Add(ped);
                }
                foreach (DataRow row in dtTieGirder.Select(string.Format("FILE_ID='{0}'", item.FILE_ID)))
                {
                    TieGirderInfo tg = row["Entity"] as TieGirderInfo;
                    tglist.Add(tg);
                }
                */

                // Select 된 것으로 기준 변경
                foreach (var handle in gvFoundation.GetSelectedRows())
                {
                    if (handle < 0) continue;

                    DataRow row = gvFoundation.GetDataRow(handle) as DataRow;
                    var fdn = row["Entity"] as FdnInfo;

                    // 2020-02-21 File 별로 ConvertList 에 담아야함. 아니면 계속 같은항목이 중복되어 들어가므로.
                    //if (ownFile && item.FILE_ID != fdn.FILE_ID) continue;

                    if (fdn.FILE_ID == null)
                    {
                        fdn.Result = ConvertResult.None;

                        flist.Add(fdn);
                    }

                    if (item.FILE_ID != fdn.FILE_ID) continue;

                    fdn.Result = ConvertResult.None;

                    flist.Add(fdn);
                }

                foreach (var handle in gvPedestal.GetSelectedRows())
                {
                    if (handle < 0) continue;

                    DataRow row = gvPedestal.GetDataRow(handle) as DataRow;
                    var ped = row["Entity"] as FdnPedInfo;

                    if (ped.FILE_ID == null)
                    {
                        ped.Result = ConvertResult.None;

                        plist.Add(ped);
                    }

                    //if (ownFile && item.FILE_ID != ped.FILE_ID) continue;
                    if (item.FILE_ID != ped.FILE_ID) continue;

                    ped.Result = ConvertResult.None;

                    // ped는 fdn의 System을 따름
                    //if (ped.System == null)
                    //{
                    //    var tmp = s3dPath.FirstOrDefault(x => x.ID == ped.SystemID);
                    //    if (tmp != null) ped.System = tmp.S3DSystem;
                    //}

                    plist.Add(ped);
                }

                foreach (var handle in gvTieGirder.GetSelectedRows())
                {
                    if (handle < 0) continue;

                    DataRow row = gvTieGirder.GetDataRow(handle) as DataRow;
                    var tie = row["Entity"] as TieGirderInfo;

                    if (tie.FILE_ID == null)
                    {
                        tie.Result = ConvertResult.None;

                        tglist.Add(tie);
                    }

                    //if (ownFile && item.FILE_ID != tie.FILE_ID) continue;
                    if (item.FILE_ID != tie.FILE_ID) continue;

                    tie.Result = ConvertResult.None;

                    tglist.Add(tie);
                }

                item.FDN_LISTS = flist;
                item.FDNPED_LISTS = plist;
                item.TieGirder_LISTS = tglist;
            }

            // Converter 파일 검사를 파일별로 할건지에 대한 여부
            bool ownFile = rgRevisionOption.SelectedIndex == 1;
            //bool result = S3DHelper.CreatePedasFoundation(this.dtPart, this.dtPedasObject, this.ConvertList, targetItem, ownFile);

            //S3D 또는 PDMS 에게 넘길 데이터
            var param = new Tuple<DataTable, DataTable, List<PedasFileInfo>, PedasFileInfo, bool>(this.dtPart, this.dtPedasObject, this.ConvertList, targetItem, ownFile);

            //S3D 혹은 PDMS 에게 명령 내림.
            bool result = false;
            this.wizardPage1.AllowCancel = false;
            this.ConverterRequestToPDMS(targetItem, ownFile);           

            //컨버팅 성공시 변화된것을 그리드뷰에 알려줌
            if (result)
            {
                ConvertAction = true;

                gvFoundation.Columns["Result"].Visible = true;
                gvPedestal.Columns["Result"].Visible = true;
                gvTieGirder.Columns["Result"].Visible = true;

                gvFoundation.Columns["PropertyStatus"].VisibleIndex = gvFoundation.Columns["RevisionType"].VisibleIndex + 1;

                foreach (var item in this.ConvertList)
                {
                    foreach (var fdn in item.FDN_LISTS)
                    {
                        //var rows = dtFooting.Select(string.Format("FILE_ID = '{0}' AND FDN_ID ='{1}' AND FDN_NAME = '{2}'", fdn.FILE_ID, fdn.FDN_ID, fdn.FDN_Name));
                        var rows = dtFooting.Select(string.Format("S3D_Note = '{0}' ", fdn.S3D_Note));
                        if (rows.Length > 0)
                        {
                            // 같은게 삭제되고 추가되는 경우가 있어서 2건 이상이 나옴
                            foreach (var row in rows)
                            {
                                row["Result"] = fdn.Result;

                                if (fdn.PropertyStatus != "")
                                    row["PropertyStatus"] = fdn.PropertyStatus;
                            }
                        }
                    }

                    foreach (var ped in item.FDNPED_LISTS)
                    {
                        var rows = dtPedestal.Select(string.Format("S3D_Note = '{0}' ", ped.S3D_Note));
                        if (rows.Length > 0)
                        {
                            foreach (var row in rows)
                            {
                                row["Result"] = ped.Result;
                            }
                        }
                    }

                    foreach (var tie in item.TieGirder_LISTS)
                    {
                        var rows = dtTieGirder.Select(string.Format("S3D_Note = '{0}'", tie.S3D_Note));
                        if (rows.Length > 0)
                        {
                            foreach (var row in rows)
                            {
                                row["Result"] = tie.Result;
                            }
                        }
                    }

                    foreach (var dummy in item.DummyList)
                    {
                        var rows = dtDummy.Select(string.Format("S3D_Note = '{0}'", dummy.S3D_Note));
                        if (rows.Length > 0)
                        {
                            foreach (var row in rows)
                            {
                                row["Result"] = dummy.Result;
                            }
                        }
                    }
                }

                // Target Revision 사용안함.
                //foreach (var fdn in targetItem.FDN_LISTS)
                //{
                //    if (fdn.RevisionType == RevisionDataType.Deleted)
                //    {
                //        var rows = dtFooting.Select(string.Format("FILE_ID = '{0}' AND FDN_ID ='{1}' AND FDN_NAME = '{2}'", fdn.FILE_ID, fdn.FDN_ID, fdn.FDN_Name));
                //        if (rows.Length > 0)
                //            rows[0]["Result"] = fdn.Result;
                //    }
                //}

                //foreach (var ped in targetItem.FDNPED_LISTS)
                //{
                //    if (ped.RevisionType == RevisionDataType.Deleted)
                //    {
                //        var rows = dtPedestal.Select(string.Format("FILE_ID = '{0}' AND PED_ID ='{1}' AND PED_NAME = '{2}'", ped.FILE_ID, ped.PED_ID, ped.PED_NAME));
                //        if (rows.Length > 0)
                //            rows[0]["Result"] = ped.Result;
                //    }
                //}

                //foreach (var tie in targetItem.TieGirder_LISTS)
                //{
                //    if (tie.RevisionType == RevisionDataType.Deleted)
                //    {
                //        var rows = dtTieGirder.Select(string.Format("FILE_ID = '{0}' AND OBJECTID ='{1}'", tie.FILE_ID, tie.OBJECTID));
                //        if (rows.Length > 0)
                //            rows[0]["Result"] = tie.Result;
                //    }
                //}

                dtFooting.AcceptChanges();
                dtPedestal.AcceptChanges();
                dtTieGirder.AcceptChanges();
                dtDummy.AcceptChanges();

                this.gcFoundation.RefreshDataSource();
                this.gcPedestal.RefreshDataSource();
                this.gcTieGirder.RefreshDataSource();
                this.gcDummy.RefreshDataSource();

                this.Cursor = Cursors.Default;

                //this.Close();
            }
            else
            {
            }

            e.Cancel = true;

            mainForm.Visible = true;
            this.Visible = true;

            this.Cursor = Cursors.Default;

            this.wizardPage1.AllowBack = false;
            this.wizardPage1.AllowFinish = false;
            this.wizardControl1.CancelText = "Close";
            isClose = false;          
        }

        private void ConverterRequestToPDMS(PedasFileInfo targetItem, bool OwnFile)
        {
            /*
             *  [참고]
             *  Zone은 사용자가 설정해주는것이다. 
             *  File은 상관없고 FDN_ID 는 Foundation 하나를 의미하는데 이는 Zone이 아니라 Equipment이다. 
             *  FdnInfo의 entity들은 extrusion이고 개별 개체들은 Loop다            
             *  ConvertList는 체크박스로 선택한 객체이다.
             *  targetItem은 내보내려는 객체이다. 선택여부와는 상관없다. 
             *  PedasSystems는 이전에 PDMS에서 가져온 모든 리비전관련 객체이다. 
             */

            //Converter : PDMS에게 데이터 넘기고 그리기 요청

            try
            {
                //이전 데이터 지우기
                System.IO.DirectoryInfo di = new DirectoryInfo(this.XmlDataFolderPath_FOUNDATION);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                //파일비교함. (S3D_NOTE)
                if (this.OWN_FILE)
                {
                    #region PDMS에 삭제할 데이터 모음집
                    //선택한객체가 Delete상태이고 PDMS에 존재하는 상태이면 삭제.
                    List<(string, string)> temp_notes = new List<(string, string)>();
                    foreach (RevisionItemInfo revision_item in this.PedasSystems)
                    {
                        foreach (PedasFileInfo pedasFileInfo in this.ConvertList)
                        {
                            for (int i = pedasFileInfo.FDN_LISTS.Count - 1; i >= 0; i--)
                            {
                                if (pedasFileInfo.FDN_LISTS[i].S3D_Note == revision_item.Note
                                    && pedasFileInfo.FDN_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.FDN_LISTS[i].RevisionType == RevisionDataType.Deleted)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID));
                                    pedasFileInfo.FDN_LISTS.RemoveAt(i);
                                }
                                else if (pedasFileInfo.FDN_LISTS[i].S3D_Note == revision_item.Note
                                   && pedasFileInfo.FDN_LISTS[i].SystemID == revision_item.SystemID
                                   && pedasFileInfo.FDN_LISTS[i].RevisionType == RevisionDataType.Modified)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID));
                                }
                            }
                            for (int i = pedasFileInfo.FDNPED_LISTS.Count - 1; i >= 0; i--)
                            {
                                if (pedasFileInfo.FDNPED_LISTS[i].S3D_Note == revision_item.Note
                                    && pedasFileInfo.FDNPED_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.FDNPED_LISTS[i].RevisionType == RevisionDataType.Deleted)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID));
                                    pedasFileInfo.FDNPED_LISTS.RemoveAt(i);
                                }
                                else if (pedasFileInfo.FDNPED_LISTS[i].S3D_Note == revision_item.Note
                                    && pedasFileInfo.FDNPED_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.FDNPED_LISTS[i].RevisionType == RevisionDataType.Modified)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID));
                                }
                            }
                            for (int i = pedasFileInfo.TieGirder_LISTS.Count - 1; i >= 0; i--)
                            {
                                if (pedasFileInfo.TieGirder_LISTS[i].S3D_Note == revision_item.Note
                                    && pedasFileInfo.TieGirder_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.TieGirder_LISTS[i].RevisionType == RevisionDataType.Deleted)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID));
                                    pedasFileInfo.TieGirder_LISTS.RemoveAt(i);
                                }
                                else if (pedasFileInfo.TieGirder_LISTS[i].S3D_Note == revision_item.Note
                                    && pedasFileInfo.TieGirder_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.TieGirder_LISTS[i].RevisionType == RevisionDataType.Modified)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID));
                                }
                            }
                        }
                    }

                    if (targetItem != null)
                    {                      
                        //PDMS에서 지워야할 타겟들
                        DataSet dsTarget = new DataSet("TARGET_ITEM");
                        DataTable dtTarget = new DataTable("TARGETS");
                        dtTarget.Columns.Add("NOTE");
                        dtTarget.Columns.Add("ZONE_REFNO");
                        dsTarget.Tables.Add(dtTarget);

                        foreach (var fdnInfo in targetItem.FDN_LISTS)
                        {
                            foreach (var pfi in this.ConvertList)
                            {
                                if (pfi.FDN_LISTS.AsEnumerable().Where(r => r.S3D_Note == fdnInfo.S3D_Note).Any()
                                    && fdnInfo.RevisionType != RevisionDataType.UnChanged)
                                {
                                    DataRow drTarget = dtTarget.NewRow();
                                    drTarget["NOTE"] = fdnInfo.S3D_Note;
                                    drTarget["ZONE_REFNO"] = fdnInfo.SystemID;
                                    dtTarget.Rows.Add(drTarget);
                                }
                            }
                        }

                        foreach (var fdnPedInfo in targetItem.FDNPED_LISTS)
                        {
                            foreach (var pfi in this.ConvertList)
                            {
                                if (pfi.FDNPED_LISTS.AsEnumerable().Where(r => r.S3D_Note == fdnPedInfo.S3D_Note).Any()
                                    && fdnPedInfo.RevisionType != RevisionDataType.UnChanged)
                                {
                                    DataRow drTarget = dtTarget.NewRow();
                                    drTarget["NOTE"] = fdnPedInfo.S3D_Note;
                                    drTarget["ZONE_REFNO"] = fdnPedInfo.SystemID;
                                    dtTarget.Rows.Add(drTarget);
                                }
                            }
                        }

                        foreach (var TieGirderInfo in targetItem.TieGirder_LISTS)
                        {
                            foreach (var pfi in this.ConvertList)
                            {
                                if (pfi.TieGirder_LISTS.AsEnumerable().Where(r => r.S3D_Note == TieGirderInfo.S3D_Note).Any()
                                    && TieGirderInfo.RevisionType != RevisionDataType.UnChanged)
                                {
                                    DataRow drTarget = dtTarget.NewRow();
                                    drTarget["NOTE"] = TieGirderInfo.S3D_Note;
                                    drTarget["ZONE_REFNO"] = TieGirderInfo.SystemID;
                                    dtTarget.Rows.Add(drTarget);
                                }
                            }
                        }

                        foreach (var temp in temp_notes)
                        {
                            DataRow drTarget = dtTarget.NewRow();
                            drTarget["NOTE"] = temp.Item1;
                            drTarget["ZONE_REFNO"] = temp.Item2;
                            dtTarget.Rows.Add(drTarget);
                        }

                        SetToXmlFile(dsTarget, this.XmlDataFolderPath_FOUNDATION, XmlDataFolderPath_FOUNDATION + "\\TARGET_ITEM.xml");
                    }
                    #endregion
                }
                //파일비교 안함. (NoteWithoutFileKey)
                else if (!this.OWN_FILE)
                {
                    #region PDMS에 삭제할 데이터 모음집
                    //선택한객체가 Delete상태이고 PDMS에 존재하는 상태이면 삭제.
                    //위와는 다르게 비교만 NoteWithoutFileKey 이고 실제로 저장할때는 Note를 저장한다.
                    List<(string, string, string)> temp_notes = new List<(string, string, string)>();
                    foreach (RevisionItemInfo revision_item in this.PedasSystems)
                    {
                        foreach (PedasFileInfo pedasFileInfo in this.ConvertList)
                        {
                            for (int i = pedasFileInfo.FDN_LISTS.Count - 1; i >= 0; i--)
                            {                                
                                if (pedasFileInfo.FDN_LISTS[i].NoteWithoutFileKey == revision_item.NoteWithoutFileKey
                                    && pedasFileInfo.FDN_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.FDN_LISTS[i].RevisionType == RevisionDataType.Deleted)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID, "Footing"));
                                    pedasFileInfo.FDN_LISTS.RemoveAt(i);
                                }
                                else if (pedasFileInfo.FDN_LISTS[i].NoteWithoutFileKey == revision_item.NoteWithoutFileKey
                                    && pedasFileInfo.FDN_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.FDN_LISTS[i].RevisionType == RevisionDataType.Modified)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID, "Footing"));
                                }
                            }
                            for (int i = pedasFileInfo.FDNPED_LISTS.Count - 1; i >= 0; i--)
                            {
                                if (pedasFileInfo.FDNPED_LISTS[i].NoteWithoutFileKey == revision_item.NoteWithoutFileKey
                                    && pedasFileInfo.FDNPED_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.FDNPED_LISTS[i].RevisionType == RevisionDataType.Deleted)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID, "Pedestal"));
                                    pedasFileInfo.FDNPED_LISTS.RemoveAt(i);
                                }
                                else if (pedasFileInfo.FDNPED_LISTS[i].NoteWithoutFileKey == revision_item.NoteWithoutFileKey
                                    && pedasFileInfo.FDNPED_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.FDNPED_LISTS[i].RevisionType == RevisionDataType.Modified)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID, "Pedestal"));
                                }
                            }
                            for (int i = pedasFileInfo.TieGirder_LISTS.Count - 1; i >= 0; i--)
                            {
                                if (pedasFileInfo.TieGirder_LISTS[i].NoteWithoutFileKey == revision_item.NoteWithoutFileKey
                                    && pedasFileInfo.TieGirder_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.TieGirder_LISTS[i].RevisionType == RevisionDataType.Deleted)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID, "TieGirder"));
                                    pedasFileInfo.TieGirder_LISTS.RemoveAt(i);
                                }
                                else if (pedasFileInfo.TieGirder_LISTS[i].NoteWithoutFileKey == revision_item.NoteWithoutFileKey
                                    && pedasFileInfo.TieGirder_LISTS[i].SystemID == revision_item.SystemID
                                    && pedasFileInfo.TieGirder_LISTS[i].RevisionType == RevisionDataType.Modified)
                                {
                                    temp_notes.Add((revision_item.Note, revision_item.SystemID, "TieGirder"));
                                }
                            }
                        }
                    }

                    if (targetItem != null)
                    {
                        //PDMS에서 지워야할 타겟들
                        DataSet dsTarget = new DataSet("TARGET_ITEM");
                        DataTable dtTarget = new DataTable("TARGETS");
                        dtTarget.Columns.Add("NOTE");
                        dtTarget.Columns.Add("ZONE_REFNO");
                        dtTarget.Columns.Add("TYPE");
                        dsTarget.Tables.Add(dtTarget);

                        //Footing, SubBase
                        foreach (var fdnInfo in targetItem.FDN_LISTS)
                        {
                            foreach (var pfi in this.ConvertList)
                            {
                                if (pfi.FDN_LISTS.AsEnumerable().Where(r => r.NoteWithoutFileKey == fdnInfo.NoteWithoutFileKey && r.SystemID == fdnInfo.SystemID).Any()
                                    && fdnInfo.RevisionType != RevisionDataType.UnChanged)
                                {
                                    DataRow drTarget = dtTarget.NewRow();
                                    drTarget["NOTE"] = fdnInfo.S3D_Note;
                                    drTarget["ZONE_REFNO"] = fdnInfo.SystemID;
                                    drTarget["TYPE"] = "Footing";
                                    dtTarget.Rows.Add(drTarget);
                                }
                            }
                        }

                        //Pedestal, Groute
                        foreach (var fdnPedInfo in targetItem.FDNPED_LISTS)
                        {
                            foreach (var pfi in this.ConvertList)
                            {
                                if (pfi.FDNPED_LISTS.AsEnumerable().Where(r => r.NoteWithoutFileKey == fdnPedInfo.NoteWithoutFileKey && r.SystemID == fdnPedInfo.SystemID).Any()
                                    && fdnPedInfo.RevisionType != RevisionDataType.UnChanged)
                                {
                                    DataRow drTarget = dtTarget.NewRow();
                                    drTarget["NOTE"] = fdnPedInfo.S3D_Note;
                                    drTarget["ZONE_REFNO"] = fdnPedInfo.SystemID;
                                    drTarget["TYPE"] = "Pedestal";
                                    dtTarget.Rows.Add(drTarget);
                                }
                            }
                        }

                        //Tie-Girder
                        foreach (var TieGirderInfo in targetItem.TieGirder_LISTS)
                        {
                            foreach (var pfi in this.ConvertList)
                            {
                                if (pfi.TieGirder_LISTS.AsEnumerable().Where(r => r.NoteWithoutFileKey == TieGirderInfo.NoteWithoutFileKey && r.SystemID == TieGirderInfo.SystemID).Any()
                                     && TieGirderInfo.RevisionType != RevisionDataType.UnChanged)
                                {
                                    DataRow drTarget = dtTarget.NewRow();
                                    drTarget["NOTE"] = TieGirderInfo.S3D_Note;
                                    drTarget["ZONE_REFNO"] = TieGirderInfo.SystemID;
                                    drTarget["TYPE"] = "TieGirder";
                                    dtTarget.Rows.Add(drTarget);
                                }
                            }
                        }

                        foreach (var temp in temp_notes)
                        {
                            DataRow drTarget = dtTarget.NewRow();
                            drTarget["NOTE"] = temp.Item1;
                            drTarget["ZONE_REFNO"] = temp.Item2;
                            drTarget["TYPE"] = temp.Item3;
                            dtTarget.Rows.Add(drTarget);
                        }

                        SetToXmlFile(dsTarget, this.XmlDataFolderPath_FOUNDATION, XmlDataFolderPath_FOUNDATION + "\\TARGET_ITEM.xml");
                    }
                    #endregion
                }

                //상태가 Unchanged인것은 삭제 및 생성하지 않음.
                foreach (PedasFileInfo pedasFileInfo in this.ConvertList)
                {
                    pedasFileInfo.FDN_LISTS.RemoveAll(r => r.RevisionType == RevisionDataType.UnChanged);

                    pedasFileInfo.FDNPED_LISTS.RemoveAll(r => r.RevisionType == RevisionDataType.UnChanged);

                    pedasFileInfo.TieGirder_LISTS.RemoveAll(r => r.RevisionType == RevisionDataType.UnChanged);
                }

                //PDMS에 생성할 객체들.
                foreach (var pedasFileInfo in this.ConvertList)
                {
                    int file_no = 0;
                    //Foundation
                    foreach (var fdnInfo in pedasFileInfo.FDN_LISTS.OrderBy(row => row.FDN_ID).ToList())
                    {
                        #region table 선언
                        DataSet dsConverterDataToPDMS = new DataSet();

                        //Elements Heights                    
                        DataTable dtFoundations = new DataTable();
                        dtFoundations.TableName = TableNameForPDMS.CONVERTER_FOUNDATIONS_DATA.ToString();
                        dtFoundations.Columns.Add("S3D_NOTE");
                        dtFoundations.Columns.Add("NOTE_DIM");
                        dtFoundations.Columns.Add("REFNO");
                        dtFoundations.Columns.Add("FILE_ID");
                        dtFoundations.Columns.Add("FDN_ID");
                        dtFoundations.Columns.Add("FDN_NAME");
                        dtFoundations.Columns.Add("FOOTING_HEIGHT");
                        dtFoundations.Columns.Add("SCREEDTHK_HEIGHT");
                        dtFoundations.Columns.Add("LEANCONCRETE_HEIGHT");
                        dtFoundations.Columns.Add("CRUSHEDSTONES_HEIGHT");
                        dtFoundations.Columns.Add("PEDESTAL_HEIGHT");
                        dtFoundations.Columns.Add("GROUT_HEIGHT");
                        dtFoundations.Columns.Add("PILE_HEIGHT");
                        dsConverterDataToPDMS.Tables.Add(dtFoundations);

                        //Footing
                        DataTable dtFooting = new DataTable();
                        dtFooting.TableName = TableNameForPDMS.CONVERTER_FOOTING_DATA.ToString();
                        dtFooting.Columns.Add("FILE_ID");
                        dtFooting.Columns.Add("FDN_ID");
                        dtFooting.Columns.Add("IS_ROTATED_FOOTING");
                        dtFooting.Columns.Add("XPOINT");
                        dtFooting.Columns.Add("YPOINT");
                        dtFooting.Columns.Add("ZPOINT");
                        dtFooting.Columns.Add("XPOINT_NOT_ROTATED");
                        dtFooting.Columns.Add("YPOINT_NOT_ROTATED");
                        dtFooting.Columns.Add("ZPOINT_NOT_ROTATED");
                        dsConverterDataToPDMS.Tables.Add(dtFooting);
                        DataRow row_Footing = null;

                        //Footing_ScreedThk
                        DataTable dtSubBase = new DataTable();
                        dtSubBase.TableName = TableNameForPDMS.CONVERTER_SUBBASE_DATA.ToString();
                        dtSubBase.Columns.Add("FILE_ID");
                        dtSubBase.Columns.Add("FDN_ID");
                        dtSubBase.Columns.Add("XPOINT");
                        dtSubBase.Columns.Add("YPOINT");
                        dtSubBase.Columns.Add("ZPOINT");
                        dsConverterDataToPDMS.Tables.Add(dtSubBase);
                        DataRow row_SubBase = null;

                        //Pedestal
                        DataTable dtPedestal = new DataTable();
                        dtPedestal.TableName = TableNameForPDMS.CONVERTER_PEDESTAL_DATA.ToString();
                        dtPedestal.Columns.Add("S3D_NOTE");
                        dtPedestal.Columns.Add("NOTE_DIM");
                        dtPedestal.Columns.Add("FILE_ID");
                        dtPedestal.Columns.Add("FDN_ID");
                        dtPedestal.Columns.Add("PED_ID");
                        dtPedestal.Columns.Add("PED_NAME");
                        dtPedestal.Columns.Add("PEDESTAL_TYPE");
                        dtPedestal.Columns.Add("XPOINT");
                        dtPedestal.Columns.Add("YPOINT");
                        dtPedestal.Columns.Add("ZPOINT");
                        dtPedestal.Columns.Add("PEDESTAL_HEIGHT");
                        dtPedestal.Columns.Add("GROUT_XPOINT");
                        dtPedestal.Columns.Add("GROUT_YPOINT");
                        dtPedestal.Columns.Add("GROUT_ZPOINT");
                        dtPedestal.Columns.Add("GROUT_THK");
                        dsConverterDataToPDMS.Tables.Add(dtPedestal);
                        DataRow row_Pedestal = null;

                        //Pile
                        DataTable dtPile = new DataTable();
                        dtPile.TableName = TableNameForPDMS.CONVERTER_PILE_DATA.ToString();
                        dtPile.Columns.Add("FILE_ID");
                        dtPile.Columns.Add("FDN_ID");
                        dtPile.Columns.Add("PILE_ID");
                        dtPile.Columns.Add("PILE_SHAPE");
                        dtPile.Columns.Add("PILE_DIAMETER");
                        dtPile.Columns.Add("PILE_PREFIX");
                        dtPile.Columns.Add("PILE_NO");
                        dtPile.Columns.Add("XPOINT");
                        dtPile.Columns.Add("YPOINT");
                        dtPile.Columns.Add("ZPOINT");
                        dsConverterDataToPDMS.Tables.Add(dtPile);
                        DataRow row_Pile = null;

                        //Hole
                        DataTable dtHole = new DataTable();
                        dtHole.TableName = TableNameForPDMS.CONVERTER_HOLE_DATA.ToString();
                        dtHole.Columns.Add("FILE_ID");// 여러개의 파일들 중에 어느 Foundation인지
                        dtHole.Columns.Add("FDN_ID"); // 여러개의 Foundation 중에 어느 Hole인지
                        dtHole.Columns.Add("HOLE_NUM"); // 하나의 Foundation 에 몇번째 Hole인지 (한개의 Loop)
                        dtHole.Columns.Add("XPOINT");
                        dtHole.Columns.Add("YPOINT");
                        dtHole.Columns.Add("ZPOINT");
                        dsConverterDataToPDMS.Tables.Add(dtHole);
                        DataRow row_Hole = null;

                        //Hole_Pedestal
                        DataTable dtPedastalHole = new DataTable();
                        dtPedastalHole.TableName = TableNameForPDMS.CONVERTER_HOLE_PEDESTAL_DATA.ToString();
                        dtPedastalHole.Columns.Add("S3D_NOTE");
                        dtPedastalHole.Columns.Add("FILE_ID");// 여러개의 파일들 중에 어느 Foundation인지
                        dtPedastalHole.Columns.Add("FDN_ID"); // 여러개의 Foundation 중에 어느 Hole인지
                        dtPedastalHole.Columns.Add("PED_ID"); // 하나의 Foundation 에 몇번째 Hole인지 (한개의 Loop)
                        dtPedastalHole.Columns.Add("XPOINT");
                        dtPedastalHole.Columns.Add("YPOINT");
                        dtPedastalHole.Columns.Add("ZPOINT");
                        dtPedastalHole.Columns.Add("PEDESTAL_HEIGHT");
                        dsConverterDataToPDMS.Tables.Add(dtPedastalHole);
                        DataRow row_PedestalHole = null;

                        #endregion

                        #region Foundations
                        DataRow drFoundations = dtFoundations.NewRow();
                        drFoundations["S3D_NOTE"] = fdnInfo.S3D_Note;
                        drFoundations["NOTE_DIM"] = fdnInfo.DimensionTekla;
                        drFoundations["REFNO"] = fdnInfo.SystemID;
                        drFoundations["FILE_ID"] = fdnInfo.FILE_ID;
                        drFoundations["FDN_ID"] = fdnInfo.FDN_ID;
                        drFoundations["FDN_NAME"] = fdnInfo.FDN_Name;
                        drFoundations["FOOTING_HEIGHT"] = fdnInfo.Hf_mm;
                        drFoundations["SCREEDTHK_HEIGHT"] = fdnInfo.ScreedThk * 1000;
                        drFoundations["LEANCONCRETE_HEIGHT"] = fdnInfo.LeanConcrete * 1000;
                        drFoundations["CRUSHEDSTONES_HEIGHT"] = fdnInfo.CrushedStones * 1000;
                        drFoundations["GROUT_HEIGHT"] = fdnInfo.PedestalList_Info[0].Grout_Thk;
                        drFoundations["PILE_HEIGHT"] = fdnInfo.PileLength_mm;
                        dtFoundations.Rows.Add(drFoundations);
                        #endregion

                        #region Footing
                        //if (fdnInfo.PedestalList_Info[0].modelType == EquipmentModelType.Pipe_Sleeper)
                        //{
                        //    ////rotation이 들어가 있으면 미리 rotate된 point를 가지고있는 Revit의 데이터를 가져와서 footing을 그림
                        //    //if (fdnInfo.FdnRevitPointList != null)
                        //    //{
                        //    //    var fdnRevitAndNormalPoints =
                        //    //           fdnInfo.FdnPointList
                        //    //           .Zip(fdnInfo.FdnRevitPointList, (fdnPoint, fdnRevitPoint) => new { FdnPoint = fdnPoint, FdnRevitPoint = fdnRevitPoint });

                        //    //    foreach (var Point in fdnRevitAndNormalPoints)
                        //    //    {
                        //    //        row_Footing = dtFooting.NewRow();
                        //    //        row_Footing["FILE_ID"] = fdnInfo.FILE_ID;
                        //    //        row_Footing["FDN_ID"] = fdnInfo.FDN_ID;
                        //    //        row_Footing["IS_ROTATED_FOOTING"] = true;
                        //    //        row_Footing["XPOINT"] = Point.FdnRevitPoint.GlobalX;
                        //    //        row_Footing["YPOINT"] = Point.FdnRevitPoint.GlobalY;
                        //    //        row_Footing["ZPOINT"] = Point.FdnRevitPoint.GlobalZ - fdnInfo.Hf_mm / 1000; //Footing의 B.O.F
                        //    //        row_Footing["XPOINT_NOT_ROTATED"] = Point.FdnPoint.GlobalX;
                        //    //        row_Footing["YPOINT_NOT_ROTATED"] = Point.FdnPoint.GlobalY;
                        //    //        row_Footing["ZPOINT_NOT_ROTATED"] = Point.FdnPoint.GlobalZ;
                        //    //        //row_Footing["ZPOINT"] = fdnPoint.GlobalZ;
                        //    //        dtFooting.Rows.Add(row_Footing);
                        //    //    }
                        //    //    //foreach (var fdnPoint in fdnInfo.FdnRevitPointList)
                        //    //    //{                                    
                        //    //    //    row_Footing = dtFooting.NewRow();
                        //    //    //    row_Footing["FILE_ID"] = fdnInfo.FILE_ID;
                        //    //    //    row_Footing["FDN_ID"] = fdnInfo.FDN_ID;
                        //    //    //    row_Footing["IS_ROTATED_FOOTING"] = true;
                        //    //    //    row_Footing["XPOINT"] = fdnPoint.GlobalX;
                        //    //    //    row_Footing["YPOINT"] = fdnPoint.GlobalY;
                        //    //    //    row_Footing["ZPOINT"] = fdnPoint.GlobalZ - fdnInfo.Hf_mm / 1000; //Footing의 B.O.F
                        //    //    //    //row_Footing["ZPOINT"] = fdnPoint.GlobalZ;
                        //    //    //    dtFooting.Rows.Add(row_Footing);
                        //    //    //}
                        //    //}
                        //}
                        //else
                        //{
                        //    if (fdnInfo.FdnRevitPointList != null)
                        //    {
                        //        foreach (var fdnPoint in fdnInfo.FdnRevitPointList)
                        //        {
                        //            row_Footing = dtFooting.NewRow();
                        //            row_Footing["FILE_ID"] = fdnInfo.FILE_ID;
                        //            row_Footing["FDN_ID"] = fdnInfo.FDN_ID;
                        //            row_Footing["IS_ROTATED_FOOTING"] = false;
                        //            row_Footing["XPOINT"] = fdnPoint.GlobalX;
                        //            row_Footing["YPOINT"] = fdnPoint.GlobalY;
                        //            row_Footing["ZPOINT"] = fdnPoint.GlobalZ - fdnInfo.Hf_mm / 1000; //Footing의 B.O.F
                        //            //row_Footing["ZPOINT"] = fdnPoint.GlobalZ;
                        //            dtFooting.Rows.Add(row_Footing);
                        //        }
                        //    }
                        //}

                        if (fdnInfo.FdnRevitPointList != null)
                        {
                            foreach (var fdnPoint in fdnInfo.FdnRevitPointList)
                            {
                                row_Footing = dtFooting.NewRow();
                                row_Footing["FILE_ID"] = fdnInfo.FILE_ID;
                                row_Footing["FDN_ID"] = fdnInfo.FDN_ID;
                                //row_Footing["IS_ROTATED_FOOTING"] = false;
                                row_Footing["XPOINT"] = fdnPoint.GlobalX;
                                row_Footing["YPOINT"] = fdnPoint.GlobalY;
                                row_Footing["ZPOINT"] = fdnPoint.GlobalZ - fdnInfo.Hf_mm / 1000; //Footing의 B.O.F
                                                                                                 //row_Footing["ZPOINT"] = fdnPoint.GlobalZ;
                                dtFooting.Rows.Add(row_Footing);
                            }
                        }

                        #endregion

                        #region SubBase
                        if (fdnInfo.FdnSubBasePointList != null)
                        {
                            //SubBase
                            foreach (var fdnPoint in fdnInfo.FdnSubBasePointList)
                            {
                                row_SubBase = dtSubBase.NewRow();
                                row_SubBase["FILE_ID"] = fdnInfo.FILE_ID;
                                row_SubBase["FDN_ID"] = fdnInfo.FDN_ID;
                                row_SubBase["XPOINT"] = fdnPoint.GlobalX;
                                row_SubBase["YPOINT"] = fdnPoint.GlobalY;
                                //SubBase 의 ZPOINT는 (Footing의 B.O.F - SubBase의 높이) 로 한다. 
                                row_SubBase["ZPOINT"] = fdnInfo.FdnPointList[0].Z - (fdnInfo.Hf_mm / 1000);
                                //row_SubBase["ZPOINT"] = fdnPoint.GlobalZ;
                                dtSubBase.Rows.Add(row_SubBase);
                            }
                        }
                        #endregion

                        #region Pedestal & Pedestal Hole & Grout
                        if (fdnInfo.PedestalList_Info != null)
                        {
                            foreach (var fdnPedInfo in fdnInfo.PedestalList_Info)
                            {
                                if (fdnPedInfo.FdnPedGroutPointList.Count > 0)
                                {
                                    //Grout와 Pedestal을 하나로 묶는다.
                                    var FdnPedAndGroutPointList =
                                    fdnPedInfo.FdnPedPointList
                                    .Zip(fdnPedInfo.FdnPedGroutPointList, (pedestal, grout) => new { PedestalPoint = pedestal, GroutPoint = grout });

                                    foreach (var Point in FdnPedAndGroutPointList)
                                    {
                                        row_Pedestal = dtPedestal.NewRow();
                                        row_Pedestal["S3D_NOTE"] = fdnPedInfo.S3D_Note;
                                        row_Pedestal["NOTE_DIM"] = fdnPedInfo.DimensionTekla;
                                        row_Pedestal["FILE_ID"] = fdnPedInfo.FILE_ID;
                                        row_Pedestal["FDN_ID"] = fdnPedInfo.FDN_ID;
                                        row_Pedestal["PED_ID"] = fdnPedInfo.PED_ID;
                                        row_Pedestal["PED_NAME"] = fdnPedInfo.PED_NAME_SHORT;
                                        row_Pedestal["PEDESTAL_TYPE"] = fdnPedInfo.pedestalType;
                                        row_Pedestal["XPOINT"] = Point.PedestalPoint.GlobalX;
                                        row_Pedestal["YPOINT"] = Point.PedestalPoint.GlobalY;
                                        row_Pedestal["ZPOINT"] = Point.PedestalPoint.GlobalZ - (fdnPedInfo.Hp - fdnPedInfo.Grout_Thk) / 1000;
                                        //row_Pedestal["ZPOINT"] = Point.PedestalPoint.GlobalZ;
                                        row_Pedestal["PEDESTAL_HEIGHT"] = fdnPedInfo.Hp - fdnPedInfo.Grout_Thk;
                                        row_Pedestal["GROUT_XPOINT"] = Point.GroutPoint.GlobalX;
                                        row_Pedestal["GROUT_YPOINT"] = Point.GroutPoint.GlobalY;
                                        row_Pedestal["GROUT_ZPOINT"] = Point.GroutPoint.GlobalZ;
                                        row_Pedestal["GROUT_THK"] = fdnPedInfo.Grout_Thk;
                                        dtPedestal.Rows.Add(row_Pedestal);
                                    }
                                }
                                else //Grout 없을때
                                {
                                    foreach (var fdnPoint in fdnPedInfo.FdnPedPointList)
                                    {
                                        row_Pedestal = dtPedestal.NewRow();
                                        row_Pedestal["S3D_NOTE"] = fdnPedInfo.S3D_Note;
                                        row_Pedestal["NOTE_DIM"] = fdnPedInfo.DimensionTekla;
                                        row_Pedestal["FILE_ID"] = fdnPedInfo.FILE_ID;
                                        row_Pedestal["FDN_ID"] = fdnPedInfo.FDN_ID;
                                        row_Pedestal["PED_ID"] = fdnPedInfo.PED_ID;
                                        row_Pedestal["PED_NAME"] = fdnPedInfo.PED_NAME_SHORT;
                                        row_Pedestal["PEDESTAL_TYPE"] = fdnPedInfo.pedestalType;
                                        row_Pedestal["XPOINT"] = fdnPoint.GlobalX;
                                        row_Pedestal["YPOINT"] = fdnPoint.GlobalY;
                                        row_Pedestal["ZPOINT"] = fdnPoint.GlobalZ - fdnPedInfo.Hp / 1000;
                                        //row_Pedestal["ZPOINT"] = fdnPoint.GlobalZ;
                                        row_Pedestal["PEDESTAL_HEIGHT"] = fdnPedInfo.Hp;
                                        row_Pedestal["GROUT_XPOINT"] = 0;
                                        row_Pedestal["GROUT_YPOINT"] = 0;
                                        row_Pedestal["GROUT_ZPOINT"] = 0;
                                        row_Pedestal["GROUT_THK"] = 0;
                                        dtPedestal.Rows.Add(row_Pedestal);
                                    }
                                }

                                //pedestal 홀
                                foreach (var fdnPoint in fdnPedInfo.FdnPedHolePointList)
                                {
                                    row_PedestalHole = dtPedastalHole.NewRow();
                                    row_PedestalHole["S3D_NOTE"] = fdnPedInfo.S3D_Note;
                                    row_PedestalHole["FILE_ID"] = fdnPedInfo.FILE_ID;
                                    row_PedestalHole["FDN_ID"] = fdnPedInfo.FDN_ID;
                                    row_PedestalHole["PED_ID"] = fdnPedInfo.PED_ID;
                                    row_PedestalHole["XPOINT"] = fdnPoint.GlobalX;
                                    row_PedestalHole["YPOINT"] = fdnPoint.GlobalY;
                                    row_PedestalHole["ZPOINT"] = fdnPoint.GlobalZ; // fdnPoint.GlobalZ; PDMS의 NXtrusion특성상 0을 줘야함.
                                    row_PedestalHole["PEDESTAL_HEIGHT"] = fdnPedInfo.Hp;
                                    //_ = fdnPoint.GlobalZ - fdnInfo.Hf_mm / 1000;
                                    dtPedastalHole.Rows.Add(row_PedestalHole);
                                }
                            }
                        }
                        #endregion

                        #region  Pile
                        if (fdnInfo.PileList != null)
                        {
                            foreach (var pileInfo in fdnInfo.PileList)
                            {
                                foreach (var fdnPoint in pileInfo.FdnPilePointList)
                                {
                                    row_Pile = dtPile.NewRow();
                                    row_Pile["FILE_ID"] = pileInfo.FILE_ID;
                                    row_Pile["FDN_ID"] = pileInfo.FDNID;
                                    row_Pile["PILE_ID"] = pileInfo.PileID.ToString();
                                    row_Pile["PILE_SHAPE"] = fdnInfo.Pile_Shape.ToString();
                                    row_Pile["PILE_DIAMETER"] = fdnInfo.Pile_Diameter.ToString();
                                    row_Pile["PILE_PREFIX"] = fdnInfo.PilePrefix == null ? "" : fdnInfo.PilePrefix.ToString();
                                    row_Pile["PILE_NO"] = fdnInfo.PileNo.ToString();
                                    row_Pile["XPOINT"] = fdnPoint.GlobalX;
                                    row_Pile["YPOINT"] = fdnPoint.GlobalY;
                                    row_Pile["ZPOINT"] = fdnPoint.GlobalZ - fdnInfo.PileLength_m;
                                    //row_Pile["ZPOINT"] = fdnPoint.GlobalZ;
                                    dtPile.Rows.Add(row_Pile);
                                }
                            }
                        }
                        #endregion

                        #region Hole
                        int i = 0;
                        foreach (var fdnPointList in fdnInfo.FdnHolePointList)
                        {
                            //fdnPoint가 또 리스트인 이유는 하나의 Foundation에 hole이 여러개이기 때문임.
                            foreach (var fdnPoint in fdnPointList)
                            {
                                row_Hole = dtHole.NewRow();
                                row_Hole["FILE_ID"] = fdnInfo.FILE_ID;
                                row_Hole["FDN_ID"] = fdnInfo.FDN_ID;
                                row_Hole["HOLE_NUM"] = i;
                                row_Hole["XPOINT"] = fdnPoint.GlobalX;
                                row_Hole["YPOINT"] = fdnPoint.GlobalY;
                                row_Hole["ZPOINT"] = fdnPoint.GlobalZ; //PDMS의 NXtrusion특성상 0을 줘야함.
                                //_ = fdnPoint.GlobalZ - fdnInfo.Hf_mm / 1000; 
                                dtHole.Rows.Add(row_Hole);
                            }
                            i++;
                        }
                        #endregion                        

                        SetToXmlFile(dsConverterDataToPDMS, XmlDataFolderPath_FOUNDATION, XmlDataFolderPath_FOUNDATION + "\\ConverterDatToPDMS" + file_no++.ToString() + ".xml");
                    }

                    //TieGirder
                    if (pedasFileInfo.TieGirder_LISTS != null)
                    {
                        foreach (var tieGirderInfo in pedasFileInfo.TieGirder_LISTS.OrderBy(row => row.OBJECTID).ToList())
                        {
                            DataSet dsConverterDataToPDMS = new DataSet();
                            //TieGirder
                            DataTable dtTieGirder = new DataTable();
                            dtTieGirder.TableName = TableNameForPDMS.CONVERTER_TIEGIRDER_DATA.ToString();
                            dtTieGirder.Columns.Add("S3D_NOTE");
                            dtTieGirder.Columns.Add("NOTE_DIM");
                            dtTieGirder.Columns.Add("REFNO");
                            dtTieGirder.Columns.Add("FILE_ID");
                            dtTieGirder.Columns.Add("FDN_ID");
                            dtTieGirder.Columns.Add("TIEGIRDER_NAME");
                            dtTieGirder.Columns.Add("XPOINT");
                            dtTieGirder.Columns.Add("YPOINT");
                            dtTieGirder.Columns.Add("ZPOINT");
                            dtTieGirder.Columns.Add("TIEGIRDER_HEIGHT");
                            dsConverterDataToPDMS.Tables.Add(dtTieGirder);
                            DataRow row_TieGirder = null;

                            foreach (var tieGirderPointInfo in tieGirderInfo.TGPoints)
                            {
                                row_TieGirder = dtTieGirder.NewRow();
                                row_TieGirder["S3D_NOTE"] = tieGirderInfo.S3D_Note;
                                row_TieGirder["NOTE_DIM"] = tieGirderInfo.DimensionTekla;
                                row_TieGirder["REFNO"] = tieGirderInfo.SystemID;
                                row_TieGirder["FILE_ID"] = tieGirderInfo.FILE_ID;
                                row_TieGirder["FDN_ID"] = tieGirderInfo.OBJECTID;
                                row_TieGirder["TIEGIRDER_NAME"] = tieGirderInfo.Name;
                                row_TieGirder["XPOINT"] = tieGirderPointInfo.GlobalX;
                                row_TieGirder["YPOINT"] = tieGirderPointInfo.GlobalY;
                                row_TieGirder["ZPOINT"] = tieGirderPointInfo.GlobalZ - tieGirderInfo.Height; //Footing의 B.O.F
                                //row_TieGirder["ZPOINT"] = tieGirderPointInfo.GlobalZ; //Footing의 B.O.F
                                row_TieGirder["TIEGIRDER_HEIGHT"] = tieGirderInfo.Height * 1000; //단위 mm
                                dtTieGirder.Rows.Add(row_TieGirder);
                            }

                            SetToXmlFile(dsConverterDataToPDMS, XmlDataFolderPath_FOUNDATION, XmlDataFolderPath_FOUNDATION + "\\ConverterDatToPDMS" + file_no++.ToString() + ".xml");
                        }
                    }
                }

                //선택한 Object PDMS에게 알려줌.
                DataSet dsPedas_Object = new DataSet();
                dsPedas_Object.DataSetName = "PEDAS_OBJECT";
                DataTable dtCopy = dtPedasObject.Copy();
                dtCopy.TableName = "PEDAS_OBJECT";
                dsPedas_Object.Tables.Add(dtCopy);

                SetToXmlFile(dsPedas_Object, XmlDataFolderPath_FOUNDATION, XmlDataFolderPath_FOUNDATION + "\\ConverterDatToPDMS" + "_PedasObject" + ".xml");

                //PDMS에게 드로잉 요청.
                clsBasic.SetRegistryValue("PDMSCommand", CommandMessage.CONVERTER_DRAW_REQUEST.ToString());

                //PDMS Drawing 완료시까지 대기(해제 타이밍은 타이머 이벤트).
                SplashScreen13.Wait.Show(this, "PDMS Element Drawing...");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }

            void SetToXmlFile(DataSet ds, string folderPath, string filePath)
            {
                //1. 데이터 저장할 디렉터리 생성
                if (Directory.Exists(folderPath) == false)
                {
                    Directory.CreateDirectory(folderPath);
                }

                //2. 완성된 데이터셋 PDMS가 읽을 수 있도록 XML변환
                ds.WriteXml(filePath);
            }
        }

        private void WizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            this.SelfForm = null;
            isClose = true;
            this.Close();
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            string title = "Export Revision List";
            sfd.Title = title;
            sfd.Filter = "Excel files (*.xlsx)|*.xlsx|Excle Files (*.xlsx)|*.xlsx";

            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH/mm");

            sfd.FileName = string.Format("{0}_{1}.xlsx", "Revision List", dateTime);

            if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                ExcelModule.SpreadExcel.HSpreadExcelExport export = new ExcelModule.SpreadExcel.HSpreadExcelExport();

                this.ColumnSetting(gvFoundation.Columns);
                this.ColumnSetting(gvPedestal.Columns);
                this.ColumnSetting(gvTieGirder.Columns, true);

                var ssc1 = export.GridControlExcelExport(gvFoundation, "Footing List", "Footing List");
                var ssc2 = export.GridControlExcelExport(gvPedestal, "Pedestal List", "Pedestal List");
                var ssc3 = export.GridControlExcelExport(gvTieGirder, "Tie-Girder List", "Tie-Girder List");

                var workSheet_Ped = ssc1.Document.Worksheets.Add("Pedestal List");
                var workSheet_Tie = ssc1.Document.Worksheets.Add("Tie-Girder List");

                workSheet_Ped.CopyFrom(ssc2.ActiveWorksheet);
                workSheet_Tie.CopyFrom(ssc3.ActiveWorksheet);

                ssc1.Document.Worksheets.ActiveWorksheet = ssc1.Document.Worksheets[0];

                ssc1.SaveDocument(sfd.FileName);
                this.Cursor = Cursors.Default;

                if (MessageBox.Show("Exporting to Excel is finished.\nDo you want to open now?", Common.ProgramName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
        }

        private void ColumnSetting(DevExpress.XtraGrid.Columns.GridColumnCollection gridColumnCollection, bool isTG = false)
        {
            if (isTG)
            {
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridColumnCollection)
                {
                    if (col.FieldName == "Width_mm" || col.FieldName == "Height_mm"
                        || col.FieldName == "Start_Offset_mm" || col.FieldName == "End_Offset_mm")
                    {
                        col.DisplayFormat.FormatString = "f0";
                    }
                }
            }
            else
            {
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridColumnCollection)
                {
                    if (col.Caption.Contains("(m)"))
                        col.DisplayFormat.FormatString = "f3";
                    else if (col.Caption.Contains("(mm)"))
                        col.DisplayFormat.FormatString = "f0";
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.Controls.Remove(pnlBack);
            this.pnlBack.Visible = false;
            this.pnlBack.SendToBack();
            this.Size = new Size(Step2Width, FormHeight);
            this.Location = this.NowPoint;
            //mainForm.WindowState = FormWindowState.Normal;
            this.Cursor = Cursors.Default;
        }

        private void WizardControl1_CustomizeCommandButtons(object sender, DevExpress.XtraWizard.CustomizeCommandButtonsEventArgs e)
        {
            e.CancelButton.Image = Properties.Resources.close_16x16;
            e.FinishButton.Image = Properties.Resources.pedas_converter_16;
        }

        private void BtnSetS3DCreateNode_Click(object sender, EventArgs e)
        {
            frmSP3DEntityPath frm = new frmSP3DEntityPath();
            frm.ShowDialog(this);
        }

        private void RgDesignExplorerOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup radio = sender as RadioGroup;
            if (radio.SelectedIndex == 0)
            {
                for (int i = 0; i < dtSystemPath.Rows.Count; i++)
                {
                    dtSystemPath.Rows[i]["Check"] = true;
                }
            }
            else if (radio.SelectedIndex == 1)
            {
                if (this.checkedDesignExplorer.Any())
                {
                    for (int i = 0; i < dtSystemPath.Rows.Count; i++)
                    {
                        dtSystemPath.Rows[i]["Check"] = false;
                    }
                    foreach (var idx_checked in this.checkedDesignExplorer)
                    {
                        dtSystemPath.Rows[idx_checked]["Check"] = true;
                    }
                }
            }

            //foreach (DataRow row in dtSystemPath.Rows)
            //{
            //    row["Name"] = string.Format("{0} [PDMS : {1} / Converter : {2}]", row["Name"], row["ChildSystemCount_PDMS_Convert"], row["ChildSystemCount"]);
            //}

            this.tlSystemPath.DataSource = dtSystemPath;
            this.tlSystemPath.RefreshDataSource();
        }
        #endregion

        #region Convert Option Page (Tree List)  

        private void InitDataTotlSystmePath()
        {
            tlObject.ParentFieldName = "ParentID";
            tlObject.KeyFieldName = "ID";
            tlObject.DataSource = dtPedasObject;
            tlObject.Columns["Count"].Visible = false;
            tlObject.Columns["Type"].Visible = false;

            tlObject.Columns["CHK"].VisibleIndex = 0;
            tlObject.Columns["NAME"].VisibleIndex = 1;

            tlObject.Columns["CHK"].Caption = "Check";
            tlObject.Columns["NAME"].Caption = "Name";

            tlObject.Columns["CHK"].Width = 20;
            tlObject.Columns["NAME"].Width = 220;

            tlObject.ExpandAll();


            tlSystemPath.ParentFieldName = "ParentID";
            tlSystemPath.KeyFieldName = "ID";
            tlSystemPath.DataSource = dtSystemPath;
            tlSystemPath.Columns["Smart3DSystemType"].Visible = false;
            tlSystemPath.Columns["ChildSystemCount"].Visible = false;
            tlSystemPath.Columns["Check"].Caption = " ";
            tlSystemPath.Columns["ChildSystemCount_PDMS_Convert"].Visible = false;
            tlSystemPath.Columns["Name"].Caption = "PDMS Path Name";
            tlSystemPath.Columns["Check"].Width = 15;
            tlSystemPath.Columns["Name"].Width = 250;
            tlSystemPath.ExpandAll();                    
        }

        #region S3D System Path
        private void TlSystemPath_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            // Check 여부에 따라 Count 다시 계산
            InitConvertPageControl();
        }

        private void TlSystemPath_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Check")
            {
                string id = e.Node.GetValue("ID").ToString();
                bool chk = Convert.ToBoolean(e.Value);

                CheckChildItem(dtSystemPath, id, chk);
            }
        }

        private void CheckChildItem(DataTable dt, string id, bool chk)
        {
            var childRows = dt.Select("ParentID = '" + id + "'");
            if (childRows.Length == 0) return;

            foreach (var row in childRows)
            {
                row["Check"] = chk;

                CheckChildItem(dt, row["ID"].ToString(), chk);
            }
        }

        private void TlSystemPath_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (tlSystemPath.FocusedColumn.FieldName == "Check")
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void TlSystemPath_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            if (e.Node == null) return;

            int selectedIndex = -1;
            try
            {
                var rowView = tlSystemPath.GetDataRecordByNode(e.Node) as DataRowView;

                selectedIndex = GetSystemImage(rowView.Row);

                e.NodeImageIndex = selectedIndex;
            }
            catch (Exception ex) { }
        }

        private void TlSystemPath_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            //if (e.Node == null || e.Column == null) return;

            //var item = tlSystemPath.GetDataRecordByNode(e.Node) as DataRowView;
            //if (item == null) return;

            //if (e.Column.FieldName == "Name")
            //{
            //    e.CellText = string.Format("{0} [PDMS : {1} / Converter : {2}]", e.CellValue, item.Row["ChildSystemCount_PDMS_Convert"], item.Row["ChildSystemCount"]);
            //}
        }

        private void TlSystemPath_GetNodeDisplayValue(object sender, DevExpress.XtraTreeList.GetNodeDisplayValueEventArgs e)
        {
            if (e.Node == null || e.Column == null) return;

            var item = tlSystemPath.GetDataRecordByNode(e.Node) as DataRowView;
            if (item == null) return;

            if (e.Column.FieldName == "Name")
            {
                e.Value = string.Format("{0} [PDMS : {1} / Converter : {2}]", e.Value, item.Row["ChildSystemCount_PDMS_Convert"], item.Row["ChildSystemCount"]);
            }
        }
        #endregion

        #region Object Option
        private void TlObject_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node == null || e.Column == null) return;

            if (e.Node.GetValue("NAME").ToString() == "Footing")
            {
                e.Appearance.BackColor = Color.WhiteSmoke;
                e.Node.Checked = true;
            }

            if (e.Column.FieldName == "NAME")
            {
                string value = e.Node.GetDisplayText(e.Column);
                string count = e.Node.GetDisplayText("Count");

                if (string.IsNullOrEmpty(value)) return;

                Rectangle bounds = e.Bounds;
                e.Column.AppearanceCell.FillRectangle(e.Cache, e.Bounds);

                string name = e.CellValue.ToString();

                if (name == "Footing")
                {
                    e.Cache.Graphics.DrawImage(Properties.Resources.Footing, bounds.X, bounds.Y + 2, 16, 16);
                    e.Cache.Graphics.DrawString(string.Format("　 {0} [{1}]", value, count), e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
                else if (name == "Pile")
                {
                    e.Cache.Graphics.DrawImage(Properties.Resources.Pile_icon_수정, bounds.X, bounds.Y + 2, 16, 16);
                    e.Cache.Graphics.DrawString(string.Format("　 {0} [{1}]", value, count), e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
                else if (name == "SubBase")
                {
                    e.Cache.Graphics.DrawImage(Properties.Resources.SubBase, bounds.X, bounds.Y + 2, 16, 16);
                    e.Cache.Graphics.DrawString(string.Format("　 {0} [{1}]", value, count), e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
                else if (name == "Pedestal")
                {
                    e.Cache.Graphics.DrawImage(Properties.Resources.Pedestal, bounds.X, bounds.Y + 2, 16, 16);
                    e.Cache.Graphics.DrawString(string.Format("　 {0} [{1}]", value, count), e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
                else if (name == "TieGirder")
                {
                    e.Cache.Graphics.DrawImage(Properties.Resources.Tie_Girder, bounds.X, bounds.Y + 2, 16, 16);
                    e.Cache.Graphics.DrawString(string.Format("　 {0} [{1}]", value, count), e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
            }
        }

        private void TlObject_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (tlObject.FocusedColumn.FieldName == "NAME")
                e.Cancel = true;

            if (tlObject.FocusedNode.GetValue("NAME").ToString() == "Footing")
                e.Cancel = true;
        }

        private void TlObject_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "CHK")
            {
                string type = e.Node.GetValue("NAME").ToString();
                bool chk = Convert.ToBoolean(e.Value);

                foreach (var row in dtPedasObject.AsEnumerable().Where(x => x["Type"].ToString() == type))
                {
                    row["CHK"] = chk;
                }
            }
        }
        #endregion

        #endregion

        #region 페이지 초기화

        /// <summary>
        /// dtPedasObject row["Count"]초기화
        /// </summary>
        private void InitConvertPageControl()
        {
            int FootingCount = 0, PileCount = 0, SubBaseCount = 0, PedestalCount = 0, TieGirderCount = 0;

            dtSystemPath.AcceptChanges();

            Action<PedasFileInfo> GetCount = (info) =>
            {
                foreach (var fdn in info.FDN_LISTS)
                {
                    if (string.IsNullOrEmpty(fdn.SystemID)) continue;

                    if (dtSystemPath.Select(string.Format("ID='{0}' and Check = true", fdn.SystemID)).Length > 0)
                    {
                        FootingCount++;
                        PileCount += fdn.Pile_Ea;
                        if (fdn.LeanConcEdge > 0) SubBaseCount++;
                        if (fdn.CrushedStones > 0) SubBaseCount++;
                        if (fdn.ScreedThk > 0) SubBaseCount++;

                        PedestalCount += fdn.PedestalList_Info.Count;
                    }
                }

                foreach (var tie in info.TieGirder_LISTS)
                {
                    if (dtSystemPath.Select(string.Format("ID='{0}' and Check = true", tie.SystemID)).Length > 0)
                    {
                        TieGirderCount++;
                    }
                }
            };

            //전역 파일을 읽어서 Foundation 부속 개체들의 개수를 count한다. 모든 파일임.
            foreach (var info in PedasData.GlobalPedasInfos)
            {
                if (info.Activated == false) continue;

                GetCount(info);
            }

            var row = dtPedasObject.AsEnumerable().FirstOrDefault(x => x["Type"].ToString() == PedasFdnName.Footing.ToString());
            if (row != null) row["Count"] = FootingCount;

            row = dtPedasObject.AsEnumerable().FirstOrDefault(x => x["NAME"].ToString() == PedasFdnName.SubBase.ToString());
            if (row != null) row["Count"] = SubBaseCount;

            row = dtPedasObject.AsEnumerable().FirstOrDefault(x => x["NAME"].ToString() == PedasFdnName.Pile.ToString());
            if (row != null) row["Count"] = PileCount;

            row = dtPedasObject.AsEnumerable().FirstOrDefault(x => x["Type"].ToString() == PedasFdnName.Pedestal.ToString());
            if (row != null) row["Count"] = PedestalCount;

            row = dtPedasObject.AsEnumerable().FirstOrDefault(x => x["Type"].ToString() == PedasFdnName.TieGirder.ToString());
            if (row != null) row["Count"] = TieGirderCount;

            dtPedasObject.AcceptChanges();

            this.btnExcel.Visible = false;
        }

        /// <summary>
        /// 리비전 페이지 초기화
        /// </summary>
        private void InitRevisionPageControl()
        {
            LoadGridView(gvFoundation);
            LoadGridView(gvPedestal);
            LoadGridView(gvTieGirder);

            //리비전할때 사용할 비교대상 프로퍼티 이름들 셋팅
            this.InitComparePropertyName();

            xGridColumn_Info();
            xGridColumn_Info_Pedestal();
            xGridColumn_Info_TieGirder();
            xGridColumn_Info_Dummy();

            gvFoundation.SelectionChanged += this.GvFoundation_SelectionChanged;
            gvPedestal.SelectionChanged += this.GvPedestal_SelectionChanged;

            gvFoundation.CustomDrawGroupRow += GvFoundation_CustomDrawGroupRow;
            gvPedestal.CustomDrawGroupRow += GvPedestal_CustomDrawGroupRow;
            gvTieGirder.CustomDrawGroupRow += GvTieGirder_CustomDrawGroupRow;

            gvFoundation.FocusedRowChanged += GvSource_FocusedRowChanged;
            gvPedestal.FocusedRowChanged += GvSource_FocusedRowChanged;
            gvTieGirder.FocusedRowChanged += GvSource_FocusedRowChanged;

            gvFoundation.CustomDrawCell += this.GvFoundation_CustomDrawCell;
            gvPedestal.CustomDrawCell += this.GvPedestal_CustomDrawCell;
            gvTieGirder.CustomDrawCell += this.GvTieGirder_CustomDrawCell;

            gvFoundation.RowStyle += GvFooting_RowStyle;
            gvPedestal.RowStyle += GvFooting_RowStyle;
            gvTieGirder.RowStyle += GvFooting_RowStyle;

            gvSummary.RowStyle += this.GvSummary_RowStyle;
            gvSummary.ShowingEditor += (s, e) => { e.Cancel = true; };

            gvCompare.OptionsView.ShowGroupPanel = false;
            gvCompare.ShowingEditor += (s, e) => { e.Cancel = true; };
            gvCompare.RowStyle += this.GvCompare_RowStyle;

            xtraTabControl1.SelectedPageChanged += this.XtraTabControl1_SelectedPageChanged;
            AddTableColumn();
            this.gvSummary.OptionsView.ShowGroupPanel = false;

            dtSummary.Columns.Add("State", typeof(string));
            dtSummary.Columns.Add("Count", typeof(string));

            dtCompare.Columns.Add("Description");
            dtCompare.Columns.Add("Converter Data");
            dtCompare.Columns.Add("S3D Data");
            dtCompare.Columns["S3D Data"].Caption = "PDMS Data";                 
        }

        #region Revision 컨트롤 초기화
        private void LoadGridView(GridView gv)
        {
            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsDetail.EnableMasterViewMode = false;
            gv.OptionsView.ShowFooter = false;
            gv.IndicatorWidth = 40;
            gv.ShowingEditor += (s, e) => { e.Cancel = true; };
            gv.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;
        }

        private void GridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
        }

        private void InitComparePropertyName()
        {
            FootingPropertyName.Add("FDN_Name");
            FootingPropertyName.Add("FootShape");
            FootingPropertyName.Add("GlobalX");
            FootingPropertyName.Add("GlobalY");
            FootingPropertyName.Add("GlobalZ");
            FootingPropertyName.Add("Lx_mm");
            FootingPropertyName.Add("Ly_mm");
            FootingPropertyName.Add("Hf_mm");
            FootingPropertyName.Add("ScreedThk");
            FootingPropertyName.Add("LeanConcrete");
            FootingPropertyName.Add("CrushedStones");
            //FootingPropertyName.Add("LeanConcEdge");
            FootingPropertyName.Add("isPileFdn");
            FootingPropertyName.Add("PileLength_mm");
            FootingPropertyName.Add("Pile_Diameter");
            FootingPropertyName.Add("Pile_Shape");
            FootingPropertyName.Add("Pile_Ea");
            FootingPropertyName.Add("Pile Point Group");
            if (PedasData.ConvertProgramMode != ProgramMode.PDMS)
            {
                FootingPropertyName.Add("SystemChildPath");
            }
            FootingPropertyName.Add("PilePrefix");
            FootingPropertyName.Add("PileNo");

            FootingUserPropertyName.Add("FDN_Name");
            FootingUserPropertyName.Add("FootShape");
            FootingUserPropertyName.Add("Point Group");
            FootingUserPropertyName.Add("Hf_mm");
            FootingUserPropertyName.Add("ScreedThk");
            FootingUserPropertyName.Add("LeanConcrete");
            FootingUserPropertyName.Add("CrushedStones");
            //FootingUserPropertyName.Add("LeanConcEdge");
            FootingUserPropertyName.Add("isPileFdn");
            FootingUserPropertyName.Add("PileLength_mm");
            FootingUserPropertyName.Add("Pile_Diameter");
            FootingUserPropertyName.Add("Pile_Shape");
            FootingUserPropertyName.Add("Pile_Ea");
            FootingUserPropertyName.Add("Pile Point Group");
            if (PedasData.ConvertProgramMode != ProgramMode.PDMS)
            {
                FootingUserPropertyName.Add("SystemChildPath");
            }
            FootingUserPropertyName.Add("PilePrefix");
            FootingUserPropertyName.Add("PileNo");

            FootingRingPropertyName.Add("FDN_NAME");
            FootingRingPropertyName.Add("Lx_mm");
            if (PedasData.ConvertProgramMode != ProgramMode.PDMS)
            {
                FootingRingPropertyName.Add("SystemChildPath");
            }
            FootingRingPropertyName.Add("PilePrefix");
            FootingRingPropertyName.Add("PileNo");

            PedestalPropertyName.Add("PED_NAME");
            PedestalPropertyName.Add("GlobalX");
            PedestalPropertyName.Add("GlobalY");
            PedestalPropertyName.Add("GlobalZ");
            PedestalPropertyName.Add("Hp");
            PedestalPropertyName.Add("dx");
            PedestalPropertyName.Add("dy");
            PedestalPropertyName.Add("Shape");
            if (PedasData.ConvertProgramMode != ProgramMode.PDMS)
            {
                PedestalPropertyName.Add("SystemChildPath");
            }
            PedRingPropertyName.Add("PED_NAME");
            PedRingPropertyName.Add("dx");
            if (PedasData.ConvertProgramMode != ProgramMode.PDMS)
            {
                PedRingPropertyName.Add("SystemChildPath");
            }
            //PedestalPropertyName.Add("Hp");
            //PedestalPropertyName.Add("dx");

            //PedestalPropertyName.Add("Grout_Thk"); // Oct 는 Grout 를 그릴 수 없으므로

            TieGirderPropertyName.Add("Name");
            TieGirderPropertyName.Add("Height");
            TieGirderPropertyName.Add("Point Group");
            if (PedasData.ConvertProgramMode != ProgramMode.PDMS)
            {
                TieGirderPropertyName.Add("SystemChildPath");
            }

            ////함수 2번이상 호출시 리스트 중복 방지.
            //FootingPropertyName = FootingPropertyName.Distinct().ToList();
            //FootingUserPropertyName = FootingUserPropertyName.Distinct().ToList();
            //FootingRingPropertyName = FootingRingPropertyName.Distinct().ToList();
            //PedestalPropertyName = PedestalPropertyName.Distinct().ToList();
            //PedRingPropertyName = PedRingPropertyName.Distinct().ToList();
            //TieGirderPropertyName = TieGirderPropertyName.Distinct().ToList();
        }

        private void xGridColumn_Info()
        {
            int index = 0;
            xGridColInfo_FDNList.Clear();
            xGridColInfo_FDNList.Add(new xGridColProperties("FDNTYPE_ID", "FDNTYPE_ID", false, -1, true, 100, "C"));
            xGridColInfo_FDNList.Add(new xGridColProperties("S3D_Note", "S3D_Note", false, -1, true, 100, "C"));
            xGridColInfo_FDNList.Add(new xGridColProperties("Result", "Result", false, -1, true, 100, "C"));
            xGridColInfo_FDNList.Add(new xGridColProperties("RevisionType", "RevisionType", false, -1, true, 100, "C"));
            xGridColInfo_FDNList.Add(new xGridColProperties("FILE_ID", "FILE_ID", false, -1, true, 100, "C"));
            xGridColInfo_FDNList.Add(new xGridColProperties("FDN_ID", "FDN_ID", false, -1, true, 100, "C"));
            xGridColInfo_FDNList.Add(new xGridColProperties("SystemID", "SystemID", false, -1, true, 100, "C"));
            xGridColInfo_FDNList.Add(new xGridColProperties("SystemName", "", false, -1, true, 100, "L")); //Caption 삭제 : System Name
            xGridColInfo_FDNList.Add(new xGridColProperties("NoteWithoutFileKey", "NoteWithoutFileKey", false, -1, true, 100, "L"));

            xGridColInfo_FDNList.Add(new xGridColProperties("FDN_Name", "Foundation Name", true, index++, true, 120, "L"));
            xGridColInfo_FDNList.Add(new xGridColProperties("FootShape", "Shape", true, index++, true, 50, "C"));
            xGridColInfo_FDNList.Add(new xGridColProperties("GlobalX", "Center X (m)", true, index++, true, 80, "R", "{0:0.000#}"));
            xGridColInfo_FDNList.Add(new xGridColProperties("GlobalY", "Center Y (m)", true, index++, true, 81, "R", "{0:0.000#}"));
            xGridColInfo_FDNList.Add(new xGridColProperties("GlobalZ", "Center Z (m)", true, index++, true, 80, "R", "{0:0.000#}"));
            xGridColInfo_FDNList.Add(new xGridColProperties("Lx_mm", "Lx (mm)", true, index++, true, 75, "R"));
            xGridColInfo_FDNList.Add(new xGridColProperties("Ly_mm", "Ly (mm)", true, index++, true, 75, "R"));
            xGridColInfo_FDNList.Add(new xGridColProperties("Hf_mm", "Hf (mm)", true, index++, true, 75, "R"));
            xGridColInfo_FDNList.Add(new xGridColProperties("Hs_mm", "Hs (mm)", true, index++, true, 75, "R"));
            xGridColInfo_FDNList.Add(new xGridColProperties("ScreedThk", "ScreedThk (m)", true, index++, true, 105, "R"));
            xGridColInfo_FDNList.Add(new xGridColProperties("LeanConcrete", "LeanConcrete (m)", true, index++, true, 120, "R"));
            xGridColInfo_FDNList.Add(new xGridColProperties("CrushedStones", "CrushedStones (m)", true, index++, true, 115, "R"));
            xGridColInfo_FDNList.Add(new xGridColProperties("LeanConcEdge", "LeanConcEdge (m)", true, index++, true, 115, "R"));
            xGridColInfo_FDNList.Add(new xGridColProperties("Pile_Ea", "Pile Qty.", true, index++, true, 70, "R"));
            xGridColInfo_FDNList.Add(new xGridColProperties("PilePrefix", "PilePrefix", true, index++, true, 70, "L"));
            xGridColInfo_FDNList.Add(new xGridColProperties("PileNo", "PileNo.", true, index++, true, 70, "R"));
            xGridColInfo_FDNList.Add(new xGridColProperties("Remark", "Remark", true, index++, true, 90, "L"));
            xGridColInfo_FDNList.Add(new xGridColProperties("PropertyStatus", "Property Status", true, index++, true, 130, "L"));
        }

        private void xGridColumn_Info_Pedestal()
        {
            int index = 0;
            xGridColInfo_PEDList.Clear();
            xGridColInfo_PEDList.Add(new xGridColProperties("PEDType_ID", "PEDType_ID", false, -1, true, 100, "C"));
            xGridColInfo_PEDList.Add(new xGridColProperties("S3D_Note", "S3D_Note", false, -1, true, 100, "C"));
            xGridColInfo_PEDList.Add(new xGridColProperties("Result", "Result", false, -1, true, 100, "C"));
            xGridColInfo_PEDList.Add(new xGridColProperties("RevisionType", "RevisionType", false, -1, true, 100, "C"));
            xGridColInfo_PEDList.Add(new xGridColProperties("FILE_ID", "FILE_ID", false, -1, true, 100, "C"));
            xGridColInfo_PEDList.Add(new xGridColProperties("PED_ID", "PED_ID", false, -1, true, 100, "C"));
            xGridColInfo_PEDList.Add(new xGridColProperties("SystemID", "SystemID", false, -1, true, 100, "C"));
            xGridColInfo_PEDList.Add(new xGridColProperties("SystemName", "System Name", false, -1, true, 100, "L"));
            xGridColInfo_PEDList.Add(new xGridColProperties("NoteWithoutFileKey", "NoteWithoutFileKey", false, -1, true, 100, "L"));

            xGridColInfo_PEDList.Add(new xGridColProperties("PED_NAME", "Pedestal Name", true, index++, true, 120, "L"));
            xGridColInfo_PEDList.Add(new xGridColProperties("Shape", "Shape", true, index++, true, 80, "L"));
            xGridColInfo_PEDList.Add(new xGridColProperties("GlobalX", "Center X (m)", true, index++, true, 80, "R", "{0:0.000#}"));
            xGridColInfo_PEDList.Add(new xGridColProperties("GlobalY", "Center Y (m)", true, index++, true, 81, "R", "{0:0.000#}"));
            xGridColInfo_PEDList.Add(new xGridColProperties("GlobalZ", "Center Z (m)", true, index++, true, 80, "R", "{0:0.000#}"));
            xGridColInfo_PEDList.Add(new xGridColProperties("dx", "dx (mm)", true, index++, true, 75, "R"));
            xGridColInfo_PEDList.Add(new xGridColProperties("dy", "dy (mm)", true, index++, true, 75, "R"));
            xGridColInfo_PEDList.Add(new xGridColProperties("Hp", "Hp (mm)", true, index++, true, 80, "R"));
            xGridColInfo_PEDList.Add(new xGridColProperties("Hpa", "Hpa (mm)", true, index++, true, 85, "R"));
            xGridColInfo_PEDList.Add(new xGridColProperties("Grout_Thk", "Grout (mm)", true, index++, true, 95, "R"));
            xGridColInfo_PEDList.Add(new xGridColProperties("Remark", "Remark", true, index++, true, 90, "L"));
        }

        private void xGridColumn_Info_TieGirder()
        {
            int index = 0;
            xGridColInfo_Tiegirder.Clear();
            xGridColInfo_Tiegirder.Add(new xGridColProperties("PARENTID", "PARENTID", false, -1, true, 100, "C"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("S3D_Note", "S3D_Note", false, -1, true, 100, "C"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("Result", "Result", false, -1, true, 100, "C"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("RevisionType", "RevisionType", false, -1, true, 100, "C"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("FILE_ID", "FILE_ID", false, -1, true, 100, "C"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("OBJECTID", "OBJECTID", false, -1, true, 100, "C"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("SystemID", "SystemID", false, -1, true, 100, "C"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("SystemName", "System Name", false, -1, true, 100, "L"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("NoteWithoutFileKey", "NoteWithoutFileKey", false, -1, true, 100, "L"));

            xGridColInfo_Tiegirder.Add(new xGridColProperties("Name", "Name", true, index++, true, 100, "L"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("Start_Ped_Name", "Start Joint No", true, index++, true, 90, "L"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("End_Ped_Name", "End Joint No", true, index++, true, 90, "L"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("Width", "Width (mm)", true, index++, true, 80, "R"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("Height", "Height (mm)", true, index++, true, 80, "R"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("StartPointX", "Start X (m)", true, index++, true, 80, "R", "{0:0.000#}"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("StartPointY", "Start Y (m)", true, index++, true, 80, "R", "{0:0.000#}"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("StartPointZ", "Start Z (m)", true, index++, true, 80, "R", "{0:0.000#}"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("EndPointX", "End X (m)", true, index++, true, 80, "R", "{0:0.000#}"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("EndPointY", "End Y (m)", true, index++, true, 80, "R", "{0:0.000#}"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("EndPointZ", "End Z (m)", true, index++, true, 80, "R", "{0:0.000#}"));
            xGridColInfo_Tiegirder.Add(new xGridColProperties("Remark", "Remark", true, index++, true, 90, "L"));
        }

        private void xGridColumn_Info_Dummy()
        {
            int index = 0;
            xGridColInfo_Dummy.Clear();
            xGridColInfo_Dummy.Add(new xGridColProperties("OBJECTID", "OBJECTID", false, -1, true, 100, "C"));
            xGridColInfo_Dummy.Add(new xGridColProperties("S3D_Note", "S3D_Note", false, -1, true, 100, "C"));
            xGridColInfo_Dummy.Add(new xGridColProperties("Result", "Result", false, -1, true, 100, "C"));
            xGridColInfo_Dummy.Add(new xGridColProperties("RevisionType", "RevisionType", false, -1, true, 100, "C"));
            xGridColInfo_Dummy.Add(new xGridColProperties("FILE_ID", "FILE_ID", false, -1, true, 100, "C"));
            xGridColInfo_Dummy.Add(new xGridColProperties("SystemID", "SystemID", false, -1, true, 100, "C"));
            xGridColInfo_Dummy.Add(new xGridColProperties("SystemName", "System Name", false, -1, true, 100, "L"));
            xGridColInfo_Dummy.Add(new xGridColProperties("NoteWithoutFileKey", "NoteWithoutFileKey", false, -1, true, 100, "L"));

            xGridColInfo_Dummy.Add(new xGridColProperties("DummyName", "Name", true, index++, true, 100, "L"));
            xGridColInfo_Dummy.Add(new xGridColProperties("Shape", "Shape", true, index++, true, 80, "L"));
            xGridColInfo_Dummy.Add(new xGridColProperties("GlobalX", "Center X", true, index++, true, 70, "R"));
            xGridColInfo_Dummy.Add(new xGridColProperties("GlobalY", "Center Y", true, index++, true, 70, "R"));
            xGridColInfo_Dummy.Add(new xGridColProperties("GlobalZ", "Center Z", true, index++, true, 70, "R"));
            xGridColInfo_Dummy.Add(new xGridColProperties("PedWidth", "PedWidth", true, index++, true, 70, "R"));
            xGridColInfo_Dummy.Add(new xGridColProperties("PedHeight", "PedHeight", true, index++, true, 70, "R"));
            xGridColInfo_Dummy.Add(new xGridColProperties("PedLength", "PedLength", true, index++, true, 70, "R"));
            xGridColInfo_Dummy.Add(new xGridColProperties("FootingWidth", "FootingWidth", true, index++, true, 70, "R"));
            xGridColInfo_Dummy.Add(new xGridColProperties("FootingHeight", "FootingHeight", true, index++, true, 70, "R"));
            xGridColInfo_Dummy.Add(new xGridColProperties("FootingLength", "FootingLength", true, index++, true, 70, "R"));
            xGridColInfo_Dummy.Add(new xGridColProperties("Rotation", "Rotation", true, index++, true, 70, "R"));
            xGridColInfo_Dummy.Add(new xGridColProperties("REMARK", "Remark", true, index++, true, 90, "L"));

        }

        private void GvPedestal_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (PedasData.ConvertProgramMode == ProgramMode.PDMS)
            {
                if (e.Action == CollectionChangeAction.Refresh)
                    return;
                this.Cursor = Cursors.WaitCursor;
                SetSelectPedestal(e.ControllerRow, e.Action);
                this.Cursor = Cursors.Default;
            }
        }

        private void SetSelectPedestal(int GroupIndex, CollectionChangeAction action)
        {
            try
            {
                //그룹아이템을 선택한것이 아닐때
                if (GroupIndex >= 0)
                {
                    FdnPedInfo pedestal = gvPedestal.GetDataRow(GroupIndex)["Entity"] as FdnPedInfo;
                    if (pedestal != null)
                    {
                        for (int index = 0; index < dtFooting.Rows.Count; index++)
                        {
                            FdnInfo fdn = gvFoundation.GetDataRow(index)["Entity"] as FdnInfo;
                            if (fdn != null)
                            {
                                foreach (FdnPedInfo fdnPedInfo in fdn.PedestalList_Info)
                                {
                                    if (pedestal.NoteWithoutFileKey == fdnPedInfo.NoteWithoutFileKey.ExToString()
                                        && pedestal.SystemID == fdnPedInfo.SystemID)
                                    {
                                        if (action == CollectionChangeAction.Add)
                                            gvFoundation.SelectRow(index);
                                        else if (action == CollectionChangeAction.Remove)
                                            gvFoundation.UnselectRow(index);
                                    }
                                }
                            }
                        }
                    }
                }
                //그룹 행 선택시
                else
                {
                    for (int i = 0; i < gvPedestal.GetChildRowCount(GroupIndex); i++)
                    {
                        var tmpindex = gvPedestal.GetChildRowHandle(GroupIndex, i);

                        SetSelectPedestal(tmpindex, action);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GvFoundation_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Refresh)
                return;
            this.Cursor = Cursors.WaitCursor;
            SetSelectFoundation(e.ControllerRow, e.Action);
            this.Cursor = Cursors.Default;
        }

        private void SetSelectFoundation(int GroupIndex, CollectionChangeAction action)
        {
            try
            {
                //하나라도 바뀌었다면
                if (GroupIndex >= 0)
                {
                    FdnInfo footing = gvFoundation.GetDataRow(GroupIndex)["Entity"] as FdnInfo;
                    if (footing != null)
                    {
                        if (footing.PedestalList_Info != null)
                        {
                            foreach (var pedestal in footing.PedestalList_Info)
                            {
                                for (int index = 0; index < dtPedestal.Rows.Count; index++)
                                {
                                    FdnPedInfo focused_ped = gvPedestal.GetDataRow(index)["Entity"] as FdnPedInfo;
                                    //DataRow focusrow_Pedestal = gvPedestal.GetDataRow(index);
                                    if (focused_ped != null)
                                    {
                                        //if (pedestal.NoteWithoutFileKey == focusrow_Pedestal["NoteWithoutFileKey"].ExToString())
                                        if (pedestal.NoteWithoutFileKey == focused_ped.NoteWithoutFileKey.ExToString()
                                            && pedestal.SystemID == focused_ped.SystemID)
                                        {
                                            if (action == CollectionChangeAction.Add)
                                                gvPedestal.SelectRow(index);
                                            else if (action == CollectionChangeAction.Remove)
                                                gvPedestal.UnselectRow(index);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    for (int index1 = 0; index1 < gvFoundation.GetChildRowCount(GroupIndex); index1++)
                    {
                        var tmpindex = gvFoundation.GetChildRowHandle(GroupIndex, index1);

                        SetSelectFoundation(tmpindex, action);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GvFoundation_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;

            if (info.Column.FieldName == "FDNTYPE_ID")
            {
                int FDNTYPEID = Convert.ToInt32(gvFoundation.GetGroupRowValue(e.RowHandle, info.Column));

                FdnInfo Fdn = null;

                try
                {
                    var rows = dtFooting.Select(string.Format("FDNTYPE_ID = '{0}'", FDNTYPEID));
                    if (rows.Length < 1) return;

                    Fdn = rows[0]["Entity"] as FdnInfo;
                    string sCount = rows.Length.ToString();

                    if (Fdn.RevisionType == RevisionDataType.Deleted)
                    {
                        info.GroupText = "<color=Blue>Deleted [" + sCount + " - EA. ]</color>";
                    }
                    else
                    {
                        string pedCount = Fdn.PedestalList_Info.Count.ToString();

                        var tempType = PedasData.FdnTypeList.Where(i => i.FDNType_ID == FDNTYPEID).First();

                        string sLx = tempType.Lx.ToString();
                        string sLy = tempType.Ly.ToString();
                        string sHf = tempType.Hf.ToString();

                        int TypeIndex = PedasData.FdnTypeList.IndexOf(tempType) + 1;

                        if (string.IsNullOrEmpty(Fdn.TieFDN_ID))
                        {
                            info.GroupText = "<color=Blue>[" + sLx + " x " + sLy + " x " + sHf + " , Ped. EA. : " + pedCount + " ] [ " + sCount + " - EA. ]</color>";
                        }
                        else
                        {
                            info.GroupText = "<color=Blue>Tiegirder [" + sLx + " x " + sLy + " x " + sHf + " , Ped. EA. : " + pedCount + " ] [ " + sCount + " - EA. ]</color>";
                        }
                    }
                }
                catch { }
            }
            else if (info.Column.FieldName == "SystemName")
            {
                info.GroupText = "     " + info.GroupValueText;

                var rows = dtSystemPath.Select(string.Format("Name = '{0}'", info.GroupValueText));

                int selectedIndex = 0;
                if (rows.Length > 0)
                {
                    selectedIndex = GetSystemImage(rows[0]);
                }

                Image img = Common.GetFoundationImageCollection().Images[selectedIndex];
                e.Painter.DrawObject(e.Info);
                Point imgPos = GetImagePosition(new Point(info.ButtonBounds.Right, info.ButtonBounds.Y), e.Graphics, info.Appearance.Font, gvFoundation.GetGroupRowDisplayText(rowHandle: e.RowHandle));
                e.Graphics.DrawImage(img, new Point(info.DataBounds.X + 40, imgPos.Y));

                e.Handled = true;
            }
        }

        private static int GetSystemImage(DataRow row)
        {
            int selectedIndex = 0;
            switch (row["Smart3DSystemType"].ToString())
            {
                case "Root":
                    if (PedasData.ConvertProgramMode == ProgramMode.S3D)
                    {
                        selectedIndex = (int)FoundationImageList.Folder;
                    }
                    else if (PedasData.ConvertProgramMode == ProgramMode.PDMS)
                    {
                        selectedIndex = (int)FoundationImageList.World;
                    }
                    break;
                case "AreaSystem":
                    selectedIndex = (int)FoundationImageList.AreaSystem;
                    break;
                case "ConduitSystem":
                    selectedIndex = (int)FoundationImageList.ConduitSystem;
                    break;
                case "ElectricalSystem":
                    selectedIndex = (int)FoundationImageList.ElectricalSystem;
                    break;
                case "EquipmentSystem":
                    selectedIndex = (int)FoundationImageList.EquipmentSystem;
                    break;
                case "GenericSystem":
                    selectedIndex = (int)FoundationImageList.GenericSystem;
                    break;
                case "DuctingSystem":
                    selectedIndex = (int)FoundationImageList.DuctingSystem;
                    break;
                case "Pipeline":
                    selectedIndex = (int)FoundationImageList.Pipeline;
                    break;
                case "PipingSystem":
                    selectedIndex = (int)FoundationImageList.PipingSystem;
                    break;
                case "UnitSystem":
                    selectedIndex = (int)FoundationImageList.UnitSystem;
                    break;
                case "StructuralSystem":
                    selectedIndex = (int)FoundationImageList.StructuralSystem;
                    break;
                case "WORLD":
                    selectedIndex = (int)FoundationImageList.World;
                    break;
                case "SITE":
                    selectedIndex = (int)FoundationImageList.Site;
                    break;
                case "ZONE":
                    selectedIndex = (int)FoundationImageList.Zone;
                    break;
                case "EQUIPMENT":
                    selectedIndex = (int)FoundationImageList.Equipment;
                    break;
                case "EXTRUSION":
                    selectedIndex = (int)FoundationImageList.Extrusion;
                    break;
                default:
                    if (PedasData.ConvertProgramMode == ProgramMode.S3D)
                    {
                        selectedIndex = (int)FoundationImageList.StructuralSystem;
                    }
                    else if (PedasData.ConvertProgramMode == ProgramMode.PDMS)
                    {
                        selectedIndex = (int)FoundationImageList.Default;
                    }
                    break;
            }
            return selectedIndex;
        }

        private static Point GetImagePosition(Point buttonRightCorner, Graphics graphics, Font font, string text)
        {
            int textWidth = Convert.ToInt32(graphics.MeasureString(text, font).Width);
            int indent = 5;
            return new Point(buttonRightCorner.X + textWidth + indent, buttonRightCorner.Y);
        }

        private void GvPedestal_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;

            if (info.Column.FieldName == "PEDType_ID")
            {
                int PEDTYPEID = Convert.ToInt32(gvPedestal.GetGroupRowValue(e.RowHandle, info.Column));

                List<FdnInfo> temp = new List<FdnInfo>();

                string sCount = dtPedestal.Select(string.Format("PEDType_ID = '{0}'", PEDTYPEID)).Length.ToString();

                var tempType = PedasData.PEDTypeList.Where(i => i.PEDType_ID == PEDTYPEID).FirstOrDefault();

                if (tempType != null)
                {
                    string sdx = tempType.dx.ToString();
                    string sdy = tempType.dy.ToString();
                    string sHp = tempType.Hp.ToString();

                    int TypeIndex = PedasData.PEDTypeList.IndexOf(tempType) + 1;

                    info.GroupText = "<color=Blue>[" + sdx + " x " + sdy + " x " + sHp + " ] [ " + sCount + " - EA ]</color>";
                }

                else
                {
                    info.GroupText = "<color=Blue>Deleted [" + sCount + " - EA. ]</color>";
                }
            }
            else if (info.Column.FieldName == "SystemName")
            {
                info.GroupText = "     " + info.GroupValueText;

                var rows = dtSystemPath.Select(string.Format("Name = '{0}'", info.GroupValueText));

                int selectedIndex = 0;
                if (rows.Length > 0)
                {
                    selectedIndex = GetSystemImage(rows[0]);
                }

                Image img = Common.GetFoundationImageCollection().Images[selectedIndex];
                e.Painter.DrawObject(e.Info);
                Point imgPos = GetImagePosition(new Point(info.ButtonBounds.Right, info.ButtonBounds.Y), e.Graphics, info.Appearance.Font, gvFoundation.GetGroupRowDisplayText(rowHandle: e.RowHandle));
                e.Graphics.DrawImage(img, new Point(info.DataBounds.X + 40, imgPos.Y));

                e.Handled = true;
            }
        }

        private void GvTieGirder_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;

            if (info.Column.FieldName == "PARENTID")
            {
                try
                {
                    //string PARENTID = gvTieGirder.GetGroupRowValue(e.RowHandle, info.Column).ToString();

                    //if (PARENTID != string.Empty)
                    //{
                    //    var Fdn = Common.TieGirder_LISTS.FirstOrDefault(p => p.OBJECTID == PARENTID);

                    //    info.GroupText = "<color=Blue>[" + Fdn.Name + "]</color>";
                    //}
                    string PARENTID = gvTieGirder.GetGroupRowValue(e.RowHandle, info.Column).ToString();

                    if (PARENTID != string.Empty)
                    {
                        string tgName = gvTieGirder.GetGroupRowValue(e.RowHandle, gvTieGirder.Columns["Name"]).ToString();
                        info.GroupText = "<color=Blue>[" + tgName + "]</color>";
                    }
                    else
                    {
                        //int PEDTYPEID = Convert.ToInt32(gvTieGirder.GetGroupRowValue(e.RowHandle, info.Column));
                        ////string sCount = dtTieGirder.Select(string.Format("PARENTID = '{0}'", PEDTYPEID)).Length.ToString();
                        int sCount = dtTieGirder.AsEnumerable().Where(r => r["PARENTID"].ToString() == PARENTID).Count();

                        string tgName = gvTieGirder.GetGroupRowValue(e.RowHandle, gvTieGirder.Columns["Name"]).ToString();
                        info.GroupText = "<color=Blue>Deleted [" + sCount + " - EA. ]</color>";
                    }                   
                }
                catch (Exception ex) { }
            }
            else if (info.Column.FieldName == "SystemName")
            {
                info.GroupText = "     " + info.GroupValueText;

                var rows = dtSystemPath.Select(string.Format("Name = '{0}'", info.GroupValueText));

                int selectedIndex = 0;
                if (rows.Length > 0)
                {
                    selectedIndex = GetSystemImage(rows[0]);
                }

                Image img = Common.GetFoundationImageCollection().Images[selectedIndex];
                e.Painter.DrawObject(e.Info);
                Point imgPos = GetImagePosition(new Point(info.ButtonBounds.Right, info.ButtonBounds.Y), e.Graphics, info.Appearance.Font, gvFoundation.GetGroupRowDisplayText(rowHandle: e.RowHandle));
                e.Graphics.DrawImage(img, new Point(info.DataBounds.X + 40, imgPos.Y));

                e.Handled = true;
            }
        }

        private void GvFoundation_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridCellInfo gci = e.Cell as GridCellInfo;
            var row = gvFoundation.GetDataRow(e.RowHandle);
            var fdn = row["Entity"] as FdnInfo;

            if (fdn != null)
            {
                Rectangle bounds = e.Bounds;
                e.Column.AppearanceCell.FillRectangle(e.Cache, e.Bounds);

                // 삭제 데이터는 ImageIndex 없음
                if (e.Column.FieldName == "FDN_Name" && fdn.RevisionType != RevisionDataType.Deleted)
                {
                    e.Cache.Graphics.DrawImage(Common.GetFoundationImageCollection().Images[(int)fdn.FdnImageIndex], bounds.X, bounds.Y + 2, 16, 16);
                    e.Cache.Graphics.DrawString("　 " + e.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
                else if (e.Column.FieldName == "FootShape")
                {
                    e.Cache.Graphics.DrawImage(Common.GetShapeImage(fdn.FootShape, true), bounds.X, bounds.Y + 2, 16, 16);
                    e.Cache.Graphics.DrawString("　 " + e.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
            }
        }

        private void GvPedestal_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridCellInfo gci = e.Cell as GridCellInfo;
            var row = gvPedestal.GetDataRow(e.RowHandle);
            var ped = row["Entity"] as FdnPedInfo;

            if (ped != null)
            {
                Rectangle bounds = e.Bounds;
                e.Column.AppearanceCell.FillRectangle(e.Cache, e.Bounds);

                if (e.Column.FieldName == "PED_NAME" && ped.RevisionType != RevisionDataType.Deleted)
                {
                    Image image;

                    if (ped.modelType == EquipmentModelType.Pipe_Sleeper)
                    {
                        image = Properties.Resources.anchorForce_T_R2;
                    }
                    else if (ped.modelType == EquipmentModelType.Horizontal_Vessel || ped.modelType == EquipmentModelType.HeatExchanger)
                    {
                        image = Properties.Resources.Ped_HV_R2;
                    }
                    else if (ped.modelType == EquipmentModelType.External_Structure)
                    {
                        image = Properties.Resources.Ped_PM_column;
                    }
                    else
                    {
                        image = Common.GetShapeImage(ped.Shape, false);
                    }

                    e.Cache.Graphics.DrawImage(image, bounds.X, bounds.Y + 2, 16, 16);
                    e.Cache.Graphics.DrawString("　 " + e.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
                else if (e.Column.FieldName == "Shape")
                {
                    e.Cache.Graphics.DrawImage(Common.GetShapeImage(ped.Shape, false), bounds.X, bounds.Y + 2, 16, 16);
                    e.Cache.Graphics.DrawString("　 " + e.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
            }
        }

        private void GvTieGirder_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridCellInfo gci = e.Cell as GridCellInfo;
            var row = gvTieGirder.GetDataRow(e.RowHandle);
            var tie = row["Entity"] as TieGirderInfo;

            if (tie != null)
            {
                Rectangle bounds = e.Bounds;
                e.Column.AppearanceCell.FillRectangle(e.Cache, e.Bounds);

                if (e.Column.FieldName == "Name")
                {
                    e.Cache.Graphics.DrawImage(Properties.Resources.Tie_Grider_Icon, bounds.X, bounds.Y + 2, 16, 16);
                    e.Cache.Graphics.DrawString("　 " + e.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
            }
        }

        private void GvSource_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;

            try
            {
                DiffRevisionData(e.FocusedRowHandle, this.SourcePedasType);
            }
            catch (Exception ex)
            {
            }
        }

        private void GvFooting_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView view = sender as GridView;

            try
            {
                RevisionDataType revisionDataType = RevisionDataType.UnChanged;
                string result = string.Empty;

                if (this.SourcePedasType == PedasFdnName.Footing)
                {
                    DataRow row = view.GetDataRow(e.RowHandle) as DataRow;
                    FdnInfo fdn = row["Entity"] as FdnInfo;
                    revisionDataType = fdn.RevisionType;
                    result = fdn.Result.ToString();

                    if (string.IsNullOrEmpty(row["Remark"].ToString()) == false)
                    {
                        view.UnselectRow(e.RowHandle);
                    }
                }
                else if (this.SourcePedasType == PedasFdnName.Pedestal)
                {
                    DataRow row = view.GetDataRow(e.RowHandle) as DataRow;
                    FdnPedInfo fdn = row["Entity"] as FdnPedInfo;
                    revisionDataType = fdn.RevisionType;
                    result = fdn.Result.ToString();

                    if (string.IsNullOrEmpty(row["Remark"].ToString()) == false)
                    {
                        view.UnselectRow(e.RowHandle);
                    }
                }
                else if (this.SourcePedasType == PedasFdnName.TieGirder)
                {
                    DataRow row = view.GetDataRow(e.RowHandle) as DataRow;
                    TieGirderInfo fdn = row["Entity"] as TieGirderInfo;
                    revisionDataType = fdn.RevisionType;
                    result = fdn.Result.ToString();

                    if (string.IsNullOrEmpty(row["Remark"].ToString()) == false)
                    {
                        view.UnselectRow(e.RowHandle);
                    }
                }

                if (ConvertAction)
                {
                    if (result == "Fail")
                        e.Appearance.BackColor = Color.LightPink;
                }
                else
                {
                    switch (revisionDataType)
                    {
                        case RevisionDataType.Added:
                            e.Appearance.BackColor = Color.PowderBlue;
                            break;
                        case RevisionDataType.Modified:
                            e.Appearance.BackColor = Color.LemonChiffon;
                            break;
                        case RevisionDataType.Deleted:
                            e.Appearance.BackColor = Color.LightCoral;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GvSummary_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            DataRow row = gvSummary.GetDataRow(e.RowHandle) as DataRow;

            switch (row["State"].ToString())
            {
                case "Added":
                    e.Appearance.BackColor = Color.PowderBlue;
                    break;
                case "Modified":
                    e.Appearance.BackColor = Color.LemonChiffon;
                    break;
                case "Deleted":
                    e.Appearance.BackColor = Color.LightCoral;
                    break;
            }
        }

        private void GvCompare_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            DataRow row = gvCompare.GetDataRow(e.RowHandle) as DataRow;

            if (string.IsNullOrEmpty(row["Converter Data"].ToString()) && this.FocusedRevisionType == RevisionDataType.Deleted)
            {
                // delete
                e.Appearance.BackColor = Color.LightCoral;
            }
            else if (string.IsNullOrEmpty(row["S3D Data"].ToString()) && this.FocusedRevisionType == RevisionDataType.Added)
            {
                // add
                e.Appearance.BackColor = Color.PowderBlue;
            }
            else if (row["Converter Data"].ToString() != row["S3D Data"].ToString() && this.FocusedRevisionType == RevisionDataType.Modified)
            {
                e.Appearance.BackColor = Color.LemonChiffon;
            }
            else
            {
                e.Appearance.BackColor = Color.Transparent;
            }
        }

        private void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (PedasData.RunType == ProgramRunType.Alone)
            {
                //XtraMessageBox.Show(this, "Cannot Use S3D data in Stand-Alone Mode!", Common.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int rowHandle = -1;
            switch (e.Page.Name)
            {
                case "xtpFooting":
                    this.SourcePedasType = PedasFdnName.Footing;
                    if (gvFoundation.RowCount > 0) rowHandle = 0;
                    break;
                case "xtpPedestal":
                    this.SourcePedasType = PedasFdnName.Pedestal;
                    if (gvPedestal.RowCount > 0) rowHandle = 0;
                    break;
                case "xtpTieGirder":
                    this.SourcePedasType = PedasFdnName.TieGirder;
                    if (gvTieGirder.RowCount > 0) rowHandle = 0;
                    break;
                case "xtpDummy":
                    this.SourcePedasType = PedasFdnName.Dummy;
                    if (gvDummy.RowCount > 0) rowHandle = 0;
                    break;
            }

            try
            {
                //SetRevisionType();
                CalcSummary(this.SourcePedasType);

                DiffRevisionData(rowHandle, this.SourcePedasType);
            }
            catch
            {
                XtraMessageBox.Show(this, "TabPage Error!", Common.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DiffRevisionData(int rowHandle, PedasFdnName pedasType)
        {
            dtCompare.Clear();

            if (rowHandle < 0) return;

            if (pedasType == PedasFdnName.Footing)
            {
                DataRow row = gvFoundation.GetDataRow(rowHandle) as DataRow;
                FdnInfo fdn = row["Entity"] as FdnInfo;

                if (fdn == null) return;

                this.FocusedRevisionType = fdn.RevisionType;

                if (this.FocusedRevisionType == RevisionDataType.Added)
                {
                    dtCompare.Rows.Add(new object[] { "Foundation Name", fdn.FDN_Name, "" });
                    dtCompare.Rows.Add(new object[] { "Shape", fdn.FootShape.ToString(), "" });
                    dtCompare.Rows.Add(new object[] { "Hf", fdn.Hf_mm, "" });
                    dtCompare.Rows.Add(new object[] { "ScreedThk", fdn.ScreedThk, "" });
                    dtCompare.Rows.Add(new object[] { "LeanConcrete", fdn.LeanConcrete, "" });
                    dtCompare.Rows.Add(new object[] { "CrushedStones", fdn.CrushedStones, "" });

                    if (fdn.FootShape == PEDAS_Shape.Rng)
                    {
                        dtCompare.Rows.Add(new object[] { "Diameter", fdn.Lx_mm, "" });
                    }
                    else
                    {
                        if (fdn.FootShape == PEDAS_Shape.User)
                        {
                            dtCompare.Rows.Add(new object[] { "Point Group", "", "" });
                        }
                        else
                        {
                            dtCompare.Rows.Add(new object[] { "Lx", fdn.Lx_mm, "" });
                            dtCompare.Rows.Add(new object[] { "Ly", fdn.Ly_mm, "" });
                            dtCompare.Rows.Add(new object[] { "Center X", fdn.GlobalX, "" });
                            dtCompare.Rows.Add(new object[] { "Center Y", fdn.GlobalY, "" });
                            dtCompare.Rows.Add(new object[] { "Center Z", fdn.GlobalZ, "" });
                        }

                        if (fdn.isPileFdn)
                        {
                            dtCompare.Rows.Add(new object[] { "Pile Type", "O", "" });
                            dtCompare.Rows.Add(new object[] { "Pile Length", fdn.PileLength_mm, "" });
                            dtCompare.Rows.Add(new object[] { "Pile Diameter", fdn.Pile_Diameter, "" });
                            dtCompare.Rows.Add(new object[] { "Pile Shape", fdn.Pile_Shape, "" });
                            dtCompare.Rows.Add(new object[] { "Pile Ea", fdn.Pile_Ea, "" });
                            dtCompare.Rows.Add(new object[] { "Pile Prefix", fdn.PilePrefix, "" });
                            dtCompare.Rows.Add(new object[] { "Pile No", fdn.PileNo, "" });
                            dtCompare.Rows.Add(new object[] { "Pile Point Group", "", "" });
                        }
                    }
                }
                else if (this.FocusedRevisionType == RevisionDataType.Modified || this.FocusedRevisionType == RevisionDataType.UnChanged)
                {
                    RevisionItemInfo info;
                    if (rgRevisionOption.SelectedIndex == 1)
                    {
                        info = PedasSystems.FirstOrDefault(x => x.Note == fdn.S3D_Note);
                    }
                    else
                    {
                        // 앞의 File Key 를 제외하고 Item 을 찾아옴
                        info = PedasSystems.FirstOrDefault(x => x.NoteWithoutFileKey == fdn.NoteWithoutFileKey);
                    }

                    if (info == null) return;
                    var item = info.GraphicItem as FdnInfo;

                    dtCompare.Rows.Add(new object[] { "Foundation Name", fdn.FDN_Name, item.FDN_Name });
                    dtCompare.Rows.Add(new object[] { "Shape", fdn.FootShape.ToString(), item.FootShape.ToString() });
                    dtCompare.Rows.Add(new object[] { "Hf", fdn.Hf_mm, item.Hf_mm });
                    dtCompare.Rows.Add(new object[] { "ScreedThk", fdn.ScreedThk, item.ScreedThk });
                    dtCompare.Rows.Add(new object[] { "LeanConcrete", fdn.LeanConcrete, item.LeanConcrete });
                    dtCompare.Rows.Add(new object[] { "CrushedStones", fdn.CrushedStones, item.CrushedStones });

                    if (fdn.FootShape == PEDAS_Shape.Rng)
                    {
                        dtCompare.Rows.Add(new object[] { "Diameter", fdn.Lx_mm, item.Lx_mm });
                    }
                    else
                    {
                        if (fdn.FootShape == PEDAS_Shape.User)
                        {
                            dtCompare.Rows.Add(new object[] { "Point Group", "", "" });
                        }
                        else
                        {
                            dtCompare.Rows.Add(new object[] { "Lx", fdn.Lx_mm, item.Lx_mm });
                            dtCompare.Rows.Add(new object[] { "Ly", fdn.Ly_mm, item.Ly_mm });
                            dtCompare.Rows.Add(new object[] { "Center X", fdn.GlobalX, item.GlobalX });
                            dtCompare.Rows.Add(new object[] { "Center Y", fdn.GlobalY, item.GlobalY });
                            dtCompare.Rows.Add(new object[] { "Center Z", fdn.GlobalZ, item.GlobalZ });
                        }

                        // 둘다 Pile 이 아닌경우는 보일 필요 없음
                        if ((fdn.isPileFdn == false && item.isPileFdn == false) == false)
                        {
                            dtCompare.Rows.Add(new object[] { "Pile Type", fdn.isPileFdn ? "O" : "X", item.isPileFdn ? "O" : "X" });
                            dtCompare.Rows.Add(new object[] { "Pile Length", fdn.PileLength_mm, item.PileLength_mm });
                            dtCompare.Rows.Add(new object[] { "Pile Diameter", fdn.Pile_Diameter, item.Pile_Diameter });
                            dtCompare.Rows.Add(new object[] { "Pile Shape", fdn.Pile_Shape, item.Pile_Shape });
                            dtCompare.Rows.Add(new object[] { "Pile Ea", fdn.Pile_Ea, item.Pile_Ea });
                            dtCompare.Rows.Add(new object[] { "Pile Prefix", fdn.PilePrefix, item.PilePrefix });
                            dtCompare.Rows.Add(new object[] { "Pile No", fdn.PileNo, item.PileNo });
                            dtCompare.Rows.Add(new object[] { "Pile Point Group", "", "" });
                        }
                    }
                }
                else if (this.FocusedRevisionType == RevisionDataType.Deleted)
                {
                    dtCompare.Rows.Add(new object[] { "Foundation Name", "", fdn.FDN_Name });
                    dtCompare.Rows.Add(new object[] { "Shape", "", fdn.FootShape.ToString() });
                    dtCompare.Rows.Add(new object[] { "Hf", "", fdn.Hf_mm });
                    dtCompare.Rows.Add(new object[] { "ScreedThk", "", fdn.ScreedThk });
                    dtCompare.Rows.Add(new object[] { "LeanConcrete", "", fdn.LeanConcrete });
                    dtCompare.Rows.Add(new object[] { "CrushedStones", "", fdn.CrushedStones });

                    if (fdn.FootShape == PEDAS_Shape.Rng)
                    {
                        dtCompare.Rows.Add(new object[] { "Diameter", "", fdn.Lx_mm });
                    }
                    else
                    {
                        if (fdn.FootShape == PEDAS_Shape.User)
                        {
                            dtCompare.Rows.Add(new object[] { "Diameter", "", fdn.Lx_mm });
                        }
                        else
                        {
                            dtCompare.Rows.Add(new object[] { "Lx", "", fdn.Lx_mm });
                            dtCompare.Rows.Add(new object[] { "Ly", "", fdn.Ly_mm });
                            dtCompare.Rows.Add(new object[] { "Center X", "", fdn.GlobalX });
                            dtCompare.Rows.Add(new object[] { "Center Y", "", fdn.GlobalY });
                            dtCompare.Rows.Add(new object[] { "Center Z", "", fdn.GlobalZ });
                        }

                        if (fdn.isPileFdn)
                        {
                            dtCompare.Rows.Add(new object[] { "Pile Type", "", fdn.isPileFdn ? "O" : "X" });
                            dtCompare.Rows.Add(new object[] { "Pile Length", "", fdn.PileLength_mm });
                            dtCompare.Rows.Add(new object[] { "Pile Diameter", "", fdn.Pile_Diameter });
                            dtCompare.Rows.Add(new object[] { "Pile Shape", "", fdn.Pile_Shape });
                            dtCompare.Rows.Add(new object[] { "Pile Ea", "", fdn.Pile_Ea });
                            dtCompare.Rows.Add(new object[] { "Pile Prefix", "", fdn.PilePrefix });
                            dtCompare.Rows.Add(new object[] { "Pile No", "", fdn.PileNo });
                        }
                    }
                }
            }
            else if (pedasType == PedasFdnName.Pedestal)
            {
                DataRow row = gvPedestal.GetDataRow(rowHandle) as DataRow;
                FdnPedInfo ped = row["Entity"] as FdnPedInfo;

                if (ped == null) return;

                this.FocusedRevisionType = ped.RevisionType;

                if (this.FocusedRevisionType == RevisionDataType.Added)
                {
                    dtCompare.Rows.Add(new object[] { "Pedestal Name", ped.PED_NAME, "" });
                    dtCompare.Rows.Add(new object[] { "Shape", ped.Shape, "" });

                    if (ped.Shape == PEDAS_Shape.Rng)
                    {
                        dtCompare.Rows.Add(new object[] { "Diameter", ped.dx, "" });
                    }
                    else
                    {
                        dtCompare.Rows.Add(new object[] { "dx", ped.dx, "" });
                        dtCompare.Rows.Add(new object[] { "dy", ped.dy, "" });
                        dtCompare.Rows.Add(new object[] { "Hp", ped.Hp, "" });
                        //dtCompare.Rows.Add(new object[] { "Grout", ped.Grout_Thk, "" });
                        dtCompare.Rows.Add(new object[] { "Center X", ped.GlobalX, "" });
                        dtCompare.Rows.Add(new object[] { "Center Y", ped.GlobalY, "" });
                        dtCompare.Rows.Add(new object[] { "Center Z", ped.GlobalZ, "" });
                    }
                }
                else if (this.FocusedRevisionType == RevisionDataType.Modified || this.FocusedRevisionType == RevisionDataType.UnChanged)
                {
                    RevisionItemInfo info;
                    if (rgRevisionOption.SelectedIndex == 1)
                    {
                        info = PedasSystems.FirstOrDefault(x => x.Note == ped.S3D_Note);
                    }
                    else
                    {
                        // 앞의 File Key 를 제외하고 Item 을 찾아옴
                        info = PedasSystems.FirstOrDefault(x => x.NoteWithoutFileKey == ped.NoteWithoutFileKey);
                    }

                    if (info == null) return;
                    var item = info.GraphicItem as FdnPedInfo;

                    dtCompare.Rows.Add(new object[] { "Pedestal Name", ped.PED_NAME, item.PED_NAME });
                    dtCompare.Rows.Add(new object[] { "Shape", ped.Shape, item.Shape });

                    if (ped.Shape == PEDAS_Shape.Rng)
                    {
                        dtCompare.Rows.Add(new object[] { "Diameter", ped.dx, item.dx });
                    }
                    else
                    {
                        dtCompare.Rows.Add(new object[] { "dx", ped.dx, item.dx });
                        dtCompare.Rows.Add(new object[] { "dy", ped.dy, item.dy });
                        dtCompare.Rows.Add(new object[] { "Hp", ped.Hp, item.Hp });
                        //dtCompare.Rows.Add(new object[] { "Grout", ped.Grout_Thk, item.Grout_Thk });
                        dtCompare.Rows.Add(new object[] { "Center X", ped.GlobalX, item.GlobalX });
                        dtCompare.Rows.Add(new object[] { "Center Y", ped.GlobalY, item.GlobalY });
                        dtCompare.Rows.Add(new object[] { "Center Z", ped.GlobalZ, item.GlobalZ });
                    }
                }
                else if (this.FocusedRevisionType == RevisionDataType.Deleted)
                {
                    dtCompare.Rows.Add(new object[] { "Pedestal Name", "", ped.PED_NAME });
                    dtCompare.Rows.Add(new object[] { "Shape", "", ped.Shape.ToString() });

                    if (ped.Shape == PEDAS_Shape.Rng)
                    {
                        dtCompare.Rows.Add(new object[] { "Diameter", "", ped.dx });
                    }
                    else
                    {
                        dtCompare.Rows.Add(new object[] { "dx", "", ped.dx });
                        dtCompare.Rows.Add(new object[] { "dy", "", ped.dy });
                        dtCompare.Rows.Add(new object[] { "Hp", "", ped.Hp });
                        //dtCompare.Rows.Add(new object[] { "Grout", "", ped.Grout_Thk });
                        dtCompare.Rows.Add(new object[] { "Center X", "", ped.GlobalX });
                        dtCompare.Rows.Add(new object[] { "Center Y", "", ped.GlobalY });
                        dtCompare.Rows.Add(new object[] { "Center Z", "", ped.GlobalZ });
                    }
                }
            }
            else if (pedasType == PedasFdnName.TieGirder)
            {
                DataRow row = gvTieGirder.GetDataRow(rowHandle) as DataRow;
                TieGirderInfo tie = row["Entity"] as TieGirderInfo;

                if (tie == null) return;

                this.FocusedRevisionType = tie.RevisionType;

                if (this.FocusedRevisionType == RevisionDataType.Added)
                {
                    dtCompare.Rows.Add(new object[] { "Name", tie.Name, "" });
                    dtCompare.Rows.Add(new object[] { "Height", tie.Width, "" });
                    dtCompare.Rows.Add(new object[] { "Point Group", "", "" });
                }
                else if (this.FocusedRevisionType == RevisionDataType.Modified || this.FocusedRevisionType == RevisionDataType.UnChanged)
                {
                    RevisionItemInfo info;
                    if (rgRevisionOption.SelectedIndex == 1)
                    {
                        info = PedasSystems.FirstOrDefault(x => x.Note == tie.S3D_Note);
                    }
                    else
                    {
                        // 앞의 File Key 를 제외하고 Item 을 찾아옴
                        info = PedasSystems.FirstOrDefault(x => x.NoteWithoutFileKey == tie.NoteWithoutFileKey);
                    }

                    if (info == null) return;
                    var item = info.GraphicItem as TieGirderInfo;

                    dtCompare.Rows.Add(new object[] { "Name", tie.Name, item.Name });
                    dtCompare.Rows.Add(new object[] { "Height", tie.Height, item.Height });
                    dtCompare.Rows.Add(new object[] { "Point Group", "", "" });
                }
                else if (this.FocusedRevisionType == RevisionDataType.Deleted)
                {
                    dtCompare.Rows.Add(new object[] { "Name", "", tie.Name });
                    dtCompare.Rows.Add(new object[] { "Height", "", tie.Height });
                    dtCompare.Rows.Add(new object[] { "Point Group", "", "" });
                }
            }

            gcCompare.DataSource = dtCompare;
            gvCompare.Columns["Description"].Width = 80;
            gvCompare.Columns["Converter Data"].Width = 40;
            gvCompare.Columns["S3D Data"].Width = 40;
        }

        private void AddTableColumn()
        {
            foreach (xGridColProperties x in xGridColInfo_FDNList)
            {
                if (x.pColName == "GlobalX" || x.pColName == "GlobalY" || x.pColName == "GlobalZ")
                {
                    dtFooting.Columns.Add(x.pColName, typeof(decimal));
                }
                else
                {
                    dtFooting.Columns.Add(x.pColName, typeof(string));
                }
            }

            foreach (xGridColProperties x in xGridColInfo_PEDList)
            {
                if (x.pColName == "GlobalX" || x.pColName == "GlobalY" || x.pColName == "GlobalZ")
                {
                    dtPedestal.Columns.Add(x.pColName, typeof(decimal));
                }
                else
                {
                    dtPedestal.Columns.Add(x.pColName, typeof(string));
                }
            }

            foreach (xGridColProperties x in xGridColInfo_Tiegirder)
            {
                if (x.pColName.StartsWith("StartPoint") || x.pColName.StartsWith("EndPoint"))
                {
                    dtTieGirder.Columns.Add(x.pColName, typeof(decimal));
                }
                else
                {
                    dtTieGirder.Columns.Add(x.pColName, typeof(string));
                }
            }

            foreach (xGridColProperties x in xGridColInfo_Dummy)
            {
                dtDummy.Columns.Add(x.pColName, typeof(string));
            }

            dtFooting.Columns.Add("Entity", typeof(PedasGraphicItem));
            dtPedestal.Columns.Add("Entity", typeof(PedasGraphicItem));
            dtTieGirder.Columns.Add("Entity", typeof(PedasGraphicItem));
            dtDummy.Columns.Add("Entity", typeof(PedasGraphicItem));
        }
        #endregion // end of Revision 컨트롤 초기화

        #endregion // end of 페이지 초기화

        #region 컨택스트 메뉴 및 툴팁
        private void ToolTip_BeforeShow(object sender, DevExpress.Utils.ToolTipControllerShowEventArgs e)
        {
            e.AutoHide = false;
        }

        private void MenuFdn_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;
            if (gvFoundation.FocusedRowHandle < 0) return;

            mainForm.Cursor = Cursors.WaitCursor;

            this.NowPoint = this.Location;

            // 창 사이즈 줄이기
            this.Controls.Add(pnlBack);
            this.pnlBack.Visible = true;
            this.pnlBack.Dock = DockStyle.Fill;
            this.pnlBack.BringToFront();
            this.Size = new Size(225, 165);

            // 사용자의 현재 스크린 사이즈			
            var height = Screen.PrimaryScreen.WorkingArea.Height;

            // 왼쪽 아래 위치
            this.Location = new Point(0, height - 200);

            DataRow row = gvFoundation.GetFocusedDataRow();
            FdnInfo fdn = row["Entity"] as FdnInfo;

            if (menu.Tag != null) //.ToString() == "Eyeshot")
            {
                MainForm form = (MainForm)mainForm;
                form.ZoomObjectInEyeshot(fdn);
                //form.WindowState = FormWindowState.Minimized;
            }
            else
            {
                if (fdn.RevisionType == RevisionDataType.Added)
                {
                    mainForm.Cursor = Cursors.Default;
                    XtraMessageBox.Show(this, "There are no item in S3D.", Common.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Command3D.CommandVoid(CommandMessage.SelectToObject, new Tuple<string, string>(fdn.S3D_Note, "FO"));
                //S3DHighlightZoomObject.SelectToObject(fdn.S3D_Note, "FO");
            }

            mainForm.Cursor = Cursors.Default;
        }

        private void MenuPed_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;
            if (gvPedestal.FocusedRowHandle < 0 || menu == null) return;

            mainForm.Cursor = Cursors.WaitCursor;

            this.NowPoint = this.Location;

            // 창 사이즈 줄이기
            this.Controls.Add(pnlBack);
            this.pnlBack.Visible = true;
            this.pnlBack.Dock = DockStyle.Fill;
            this.pnlBack.BringToFront();
            this.Size = new Size(225, 165);

            // 사용자의 현재 스크린 사이즈			
            var height = Screen.PrimaryScreen.WorkingArea.Height;

            // 왼쪽 아래 위치
            this.Location = new Point(0, height - 200);

            DataRow row = gvPedestal.GetFocusedDataRow();
            FdnPedInfo ped = row["Entity"] as FdnPedInfo;

            if (menu.Tag != null) //.ToString() == "Eyeshot")
            {
                MainForm form = (MainForm)mainForm;
                form.ZoomObjectInEyeshot(ped);
                //form.WindowState = FormWindowState.Minimized;
            }
            else
            {
                if (ped.RevisionType == RevisionDataType.Added)
                {
                    mainForm.Cursor = Cursors.Default;
                    XtraMessageBox.Show(this, "There are no item in S3D.", Common.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Command3D.CommandVoid(CommandMessage.SelectToObject, new Tuple<string, string>(ped.S3D_Note, "PE"));
                //S3DHighlightZoomObject.SelectToObject(ped.S3D_Note, "PE");
            }

            mainForm.Cursor = Cursors.Default;
        }

        private void MenuTie_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;

            if (gvTieGirder.FocusedRowHandle < 0 || menu == null) return;

            mainForm.Cursor = Cursors.WaitCursor;

            this.NowPoint = this.Location;

            // 창 사이즈 줄이기
            this.Controls.Add(pnlBack);
            this.pnlBack.Visible = true;
            this.pnlBack.Dock = DockStyle.Fill;
            this.pnlBack.BringToFront();
            this.Size = new Size(225, 165);

            // 사용자의 현재 스크린 사이즈			
            var height = Screen.PrimaryScreen.WorkingArea.Height;

            // 왼쪽 아래 위치
            this.Location = new Point(0, height - 200);

            DataRow row = gvTieGirder.GetFocusedDataRow();
            TieGirderInfo tie = row["Entity"] as TieGirderInfo;

            if (menu.Tag != null) //.ToString() == "Eyeshot")
            {
                MainForm form = (MainForm)mainForm;
                form.ZoomObjectInEyeshot(tie);
                //form.WindowState = FormWindowState.Minimized;
            }
            else
            {
                if (tie.RevisionType == RevisionDataType.Added)
                {
                    mainForm.Cursor = Cursors.Default;
                    XtraMessageBox.Show(this, "There are no item in S3D.", Common.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Command3D.CommandVoid(CommandMessage.SelectToObject, new Tuple<string, string>(tie.S3D_Note, "TG"));
                //S3DHighlightZoomObject.SelectToObject(tie.S3D_Note, "TG");
            }

            mainForm.Cursor = Cursors.Default;
        }

        #endregion

        #region 리비전 관련 컨트롤 초기화 (Next 버튼 클릭시)
        private void InitGridRevision()
        {
            SetGridFoundationData();

            //StandAlone모드일때 리비전 체크 안함.
            if (PedasData.RunType == ProgramRunType.Alone)
            {
                //XtraMessageBox.Show(this, "Cannot Use S3D data in Stand-Alone Mode!", Common.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #region PATH 체크된것의 부모들의 아이디들을 합침. 부모◆부모◆...◆자신 -> systemIDs_FullIds
            var systemIDs_FullIDs = new List<string>();

            foreach (DataRow row in dtSystemPath.Select("Check = true"))
            {
                string ID = row["ID"].ToString();

                string ParentIDs = string.Empty;
                DataRow ParentRow = row;
                for (; ; )
                {
                    string tmp = ParentRow["ParentID"].ExToString();

                    if (tmp == string.Empty)
                        break;
                    else
                    {
                        ParentRow = dtSystemPath.AsEnumerable().FirstOrDefault(p => p["ID"].ExToString() == tmp);
                        ParentIDs += ParentRow["ID"].ExToString() + "◆";
                    }
                }

                //부모◆부모◆...◆자신
                systemIDs_FullIDs.Add(ParentIDs + ID);
            }
            #endregion

            SplashScreen13.Wait.Show(this, "S3D Revision Check...");

            //S3DHelper.GetStructFootingSystem(dtPedasObject, systemIDs_FullIDs, out this.PedasSystems);

            //dtPedasObject와 위의 systemIDs_FullIDs를 튜플로 묶음
            var param = new Tuple<DataTable, List<string>>(dtPedasObject, systemIDs_FullIDs);

            //PedasSystems에 리비전 리스트 셋팅
            #region 선택한 Object PDMS에게 알려줌.
            DataSet dsPedas_Object = new DataSet();
            dsPedas_Object.DataSetName = "PEDAS_OBJECT";
            DataTable dtCopy = dtPedasObject.Copy();
            dtCopy.TableName = "PEDAS_OBJECT";
            dsPedas_Object.Tables.Add(dtCopy);

            if (Directory.Exists(this.XmlDataFolderPath_FOUNDATION) == false)
            {
                Directory.CreateDirectory(XmlDataFolderPath_FOUNDATION);
            }
            dsPedas_Object.WriteXml(XmlDataFolderPath_FOUNDATION + "\\ConverterDatToPDMS" + "_PedasObject" + ".xml");
            #endregion

            #region PDMS에게 리비전 관련 데이터 요청.                
            // Converter 파일 검사를 파일별로 할건지에 대한 여부
            bool ownFile = rgRevisionOption.SelectedIndex == 1;
            clsBasic.SetRegistryValue("OWN_FILE", ownFile.ToString());
            clsBasic.SetRegistryValue("PDMSCommand", nameof(CommandMessage.CONVERTER_REVISION_REQUEST));

            //데이터 받기 전까지 Convert 막음.
            this.wizardPage1.AllowBack = false;
            this.wizardPage1.AllowFinish = false;

            //데이터 받을때까지 기다림.                
            SplashScreen13.Wait.Show(this, "Revision Request Processing...");
            #endregion
        }

        #region 관련 메서드

        private void SetGridFoundationData()
        {
            #region Foundation Grid Setting
            gcFoundation.DataSource = dtFooting;
            gvFoundation.PopulateColumns();
            this.xtpFooting.Text = string.Format("Footing [{0}]", dtFooting.Rows.Count);

            clsGrid.xGridColumn_Initialize(gvFoundation, xGridColInfo_FDNList, gcFoundation.Width);

            Common.GridGrouping(gvFoundation, "SystemName", true);
            Common.GridGrouping(gvFoundation, "FDNTYPE_ID", true);
            gvFoundation.ExpandAllGroups();

            //gvFoot.Columns["FDN_ID"].SortIndex = 0;
            gvFoundation.Columns["FDN_Name"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvFoundation.Columns["FootShape"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            gvFoundation.OptionsSelection.MultiSelect = true;
            gvFoundation.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gvFoundation.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;

            #endregion

            #region Pedestal Grid Setting
            gcPedestal.DataSource = dtPedestal;
            gvPedestal.PopulateColumns();
            this.xtpPedestal.Text = string.Format("Pedestal [{0}]", dtPedestal.Rows.Count);

            clsGrid.xGridColumn_Initialize(gvPedestal, xGridColInfo_PEDList, gcPedestal.Width);

            Common.GridGrouping(gvPedestal, "SystemName", true);
            Common.GridGrouping(gvPedestal, "PEDType_ID", true);
            gvPedestal.ExpandAllGroups();

            //gvPed.Columns["PED_ID"].SortIndex = 0;
            gvPedestal.Columns["PED_NAME"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvPedestal.Columns["Shape"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            gvPedestal.OptionsSelection.MultiSelect = true;
            gvPedestal.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gvPedestal.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;

            #endregion

            #region TieGirder Grid Setting

            gcTieGirder.DataSource = dtTieGirder;
            gvTieGirder.PopulateColumns();
            this.xtpTieGirder.Text = string.Format("Tie-Girder [{0}]", dtTieGirder.Rows.Count);

            clsGrid.xGridColumn_Initialize(gvTieGirder, xGridColInfo_Tiegirder, gcTieGirder.Width);

            Common.GridGrouping(gvTieGirder, "SystemName", true);
            Common.GridGrouping(gvTieGirder, "PARENTID", true);
            gvTieGirder.ExpandAllGroups();

            gvTieGirder.Columns["Name"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvTieGirder.Columns["Start_Ped_Name"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvTieGirder.Columns["End_Ped_Name"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            gvTieGirder.OptionsSelection.MultiSelect = true;
            gvTieGirder.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gvTieGirder.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;

            #endregion

            #region Dummy Grid Setting

            gcDummy.DataSource = dtDummy;
            gvDummy.PopulateColumns();
            this.xtpDummy.Text = string.Format("Dummy [{0}]", dtDummy.Rows.Count);

            clsGrid.xGridColumn_Initialize(gvDummy, xGridColInfo_Dummy, gcDummy.Width);

            Common.GridGrouping(gvDummy, "SystemName", true);
            gvDummy.ExpandAllGroups();

            gvDummy.Columns["DummyName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvDummy.Columns["Shape"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            gvDummy.OptionsSelection.MultiSelect = true;
            gvDummy.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gvDummy.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;

            #endregion
        }

        private void SetRevisionType_S3D()
        {
            targetFooting.Clear();
            targetPedestal.Clear();
            targetTieGirder.Clear();
            targetDummy.Clear();

            bool ownFile = rgRevisionOption.SelectedIndex == 1;
            string error = "";

            error = "As a result of the identification, several foundations exist on the S3D model with same name. So cannot be converted.";

            var DelRows = new List<DataRow>();

            #region Footing
            foreach (DataRow row in this.dtFooting.Rows)
            {
                FdnInfo fdn = row["Entity"] as FdnInfo;
                RevisionDataType revisionDataType = RevisionDataType.UnChanged;

                RevisionItemInfo item;
                if (ownFile)
                {
                    item = PedasSystems.FirstOrDefault(x => x.Note == fdn.S3D_Note);
                }
                else
                {
                    // 앞의 File Key 를 제외하고 Item 을 찾아옴
                    // 1. S3D와 비교
                    item = PedasSystems.FirstOrDefault(x => x.NoteWithoutFileKey == fdn.NoteWithoutFileKey);

                    if (PedasSystems.Count(x => x.NoteWithoutFileKey == fdn.NoteWithoutFileKey) > 1)
                    {
                        // 다른 System 에 File Key가 다르고 Note 가 같은 Foundation 이 다수 존재
                        // 이런 경우에는 Converting 을 하지 않고 Remark에 알림 표시

                        row["Remark"] = error;
                        continue;
                    }

                    if (item != null)
                    {
                        // 2. Load 된 데이터와 비교
                        if (dtFooting.Select(string.Format("NoteWithoutFileKey = '{0}'", item.NoteWithoutFileKey)).Length > 1)
                        {
                            row["Remark"] = error;
                            continue;
                        }
                    }
                }

                if (item != null)
                {
                    if (revisionDataType == RevisionDataType.UnChanged)
                    {
                        //SystemChild Path 검토
                        fdn.SystemChildPath = string.Empty;

                        var PathString = PedasData.GetSP3D_CREATEPATH(EntityType.Footing, false);

                        foreach (var txt in PathString)
                        {
                            fdn.SystemChildPath += txt.Item1 + "◆";
                        }
                    }

                    // 수정되었는지 데이터 비교 (ConvertFile 비교 아님)
                    FdnInfo targetItem = item.GraphicItem as FdnInfo;

                    if (targetItem.FootShape == PEDAS_Shape.Rng && fdn.FootShape == PEDAS_Shape.Rng)
                    {
                        if (Math.Abs(fdn.Lx_mm - targetItem.Lx_mm) <= 5)
                        {
                            targetItem.Lx_mm = fdn.Lx_mm;
                        }
                    }

                    targetFooting.Add(targetItem);

                    if (fdn.FootShape == PEDAS_Shape.User)
                    {
                        if (CompareRevision(fdn, targetItem, this.FootingUserPropertyName))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }

                        if (CompareFdnPoint(fdn.FdnPointList, targetItem.FdnPointList))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }
                    }
                    else if (fdn.FootShape == PEDAS_Shape.Rng)
                    {
                        if (CompareRevision(fdn, targetItem, this.FootingRingPropertyName))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }

                        if (CompareFdnPoint(fdn.FdnPointList, targetItem.FdnPointList))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }
                    }
                    else
                    {
                        if (CompareRevision(fdn, targetItem, this.FootingPropertyName))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }
                    }

                    if (revisionDataType == RevisionDataType.UnChanged)
                    {
                        if (fdn.isPileFdn)
                        {
                            string SystemChildPath = string.Empty;

                            var PathString = PedasData.GetSP3D_CREATEPATH(EntityType.Pile, false, targetItem.FDN_Name);

                            foreach (var txt in PathString)
                            {
                                SystemChildPath += txt.Item1 + "◆";
                            }
                            if (PedasData.ConvertProgramMode != ProgramMode.PDMS)
                            {
                                if (SystemChildPath != targetItem.PileList[0].SystemChildPath)
                                {
                                    revisionDataType = RevisionDataType.Modified;
                                }
                            }

                        }
                    }

                    // 같은 File 에서 Check 하는 것이라면
                    // S3D에 같은 데이터가 있고, System 이 다르면 삭제 및 새로운 Row로 추가
                    if (fdn.SystemID != targetItem.SystemID)
                    {
                        // 기존 데이터는 추가처리 (같은 row 삭제 Flag 로 추가)
                        revisionDataType = RevisionDataType.Added;

                        // fdn을 직접 바꿔버리면, Back 했을때 영향감.
                        var delItem = new FdnInfo();
                        fdn.CloneData(delItem);

                        delItem.SystemID = targetItem.SystemID;
                        delItem.SystemName = targetItem.SystemName;
                        delItem.FoundationSystemID = targetItem.FoundationSystemID;
                        delItem.BusinessObjectID = targetItem.BusinessObjectID;
                        //delItem.MainSystem = targetItem.MainSystem;
                        //delItem.FoundationSystem = targetItem.FoundationSystem;
                        //delItem.businessobject = targetItem.businessobject;

                        // 삭제 처리되는 System으로 변경
                        DelRows.Add(this.FdnInfoToDataRow(delItem));
                    }

                    // 값이 정확하지 않아서, 우선 비교하지 않음 2019-12-02
                    //// Lean Edge 는 비교는 하지만, Target 값을 가져오기 애매하여 일단 보류.
                    //// 나중에 Property 다 저장할때 그때 비교하면 될듯 by bskim
                    //if(fdn.FdnSubBasePointList != null && item.FdnSubBasePointList != null)
                    //{
                    //	double fdnX = fdn.FdnSubBasePointList.OrderBy(x => x.X).ToList()[0].X;
                    //	double targetX = item.FdnSubBasePointList.OrderBy(x => x.X).ToList()[0].X;
                    //	if (fdnX != targetX)
                    //		revisionDataType = RevisionDataType.Modified;
                    //}
                }
                else
                {
                    // 추가
                    revisionDataType = RevisionDataType.Added;
                }

                fdn.RevisionType = revisionDataType;
                row["RevisionType"] = revisionDataType;
            }

            foreach (var newRow in DelRows)
            {
                var fdn = newRow["Entity"] as FdnInfo;
                fdn.RevisionType = RevisionDataType.Deleted;
                newRow["RevisionType"] = RevisionDataType.Deleted;

                dtFooting.Rows.Add(newRow);
            }

            DelRows.Clear();
            #endregion

            #region Pedestal
            foreach (DataRow row in this.dtPedestal.Rows)
            {
                FdnPedInfo ped = row["Entity"] as FdnPedInfo;

                RevisionDataType revisionDataType = RevisionDataType.UnChanged;

                RevisionItemInfo item;

                if (ownFile)
                {
                    item = PedasSystems.FirstOrDefault(x => x.Note == ped.S3D_Note);
                }
                else
                {
                    // 앞의 File Key 를 제외하고 Item 을 찾아옴
                    item = PedasSystems.FirstOrDefault(x => x.NoteWithoutFileKey == ped.NoteWithoutFileKey);

                    if (PedasSystems.Count(x => x.NoteWithoutFileKey == ped.NoteWithoutFileKey) > 1)
                    {
                        // 다른 System 에 File Key가 다르고 Note 가 같은 Foundation 이 다수 존재
                        // 이런 경우에는 Converting 을 하지 않고 Remark에 알림 표시

                        row["Remark"] = error;
                        continue;
                    }

                    if (item != null)
                    {
                        // 2. Load 된 데이터와 비교
                        if (dtPedestal.Select(string.Format("NoteWithoutFileKey = '{0}'", item.NoteWithoutFileKey)).Length > 1)
                        {
                            row["Remark"] = error;
                            continue;
                        }
                    }
                }

                if (item != null)
                {
                    // 수정되었는지 데이터 비교
                    FdnPedInfo targetItem = item.GraphicItem as FdnPedInfo;

                    if (revisionDataType == RevisionDataType.UnChanged)
                    {
                        //SystemChild Path 검토
                        ped.SystemChildPath = string.Empty;

                        var PathString = PedasData.GetSP3D_CREATEPATH(EntityType.Pedestal, false);

                        foreach (var txt in PathString)
                        {
                            if (txt.Item2 == EntityType.Footing)
                            {
                                var entity = PedasSystems.FirstOrDefault(x => x.NoteWithoutFileKey == ped.FootingInfo.NoteWithoutFileKey);
                                FdnInfo footing = entity.GraphicItem as FdnInfo;

                                //if (footing.businessobject.GetType().ToString().Contains("Slab"))
                                if (footing.BusinessObjectTypeName.Contains("Slab"))
                                {

                                }
                                else
                                {
                                    ped.SystemChildPath += ped.FootingInfo.FDN_Name + "◆";
                                }
                            }
                            else
                                ped.SystemChildPath += txt.Item1 + "◆";
                        }
                    }

                    if (targetItem.Shape == PEDAS_Shape.Rng && ped.Shape == PEDAS_Shape.Rng)
                    {
                        if (Math.Abs(ped.dx - targetItem.dx) <= 5)
                        {
                            targetItem.dx = ped.dx;
                        }
                    }

                    targetPedestal.Add(targetItem);

                    if (ped.Shape == PEDAS_Shape.Rng)
                    {
                        if (CompareRevision(ped, targetItem, this.PedRingPropertyName))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }

                        var rows = dtFooting.Select(string.Format("FILE_ID = '{2}' AND FDN_ID ='{0}' AND RevisionType = '{1}'", ped.FDN_ID, RevisionDataType.Modified, ped.FILE_ID));

                        if (rows.Length > 0)
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }
                    }
                    else
                    {
                        // Draw 할때 Grout 두께만큼 빼서 그렸음.
                        targetItem.Hp += targetItem.Grout_Thk;
                        if (CompareRevision(ped, targetItem, this.PedestalPropertyName))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }
                        // 다시 빼준다.
                        targetItem.Hp -= targetItem.Grout_Thk;
                    }

                    // S3D에 같은 데이터가 있고, System 이 다르면 삭제 및 새로운 Row로 추가
                    if (ped.FootingInfo.SystemID != targetItem.SystemID)
                    {
                        // 기존 데이터는 추가처리 (같은 row 삭제 Flag 로 추가)
                        revisionDataType = RevisionDataType.Added;

                        // 직접 변경 시, Back 했을때 영향감.
                        var delItem = new FdnPedInfo();
                        ped.CloneData(delItem);

                        delItem.IsS3D = targetItem.IsS3D; // S3D 상태로 바꿔주고 System Name 설정
                        delItem.SystemID = targetItem.SystemID;
                        delItem.SystemName = targetItem.SystemName;
                        delItem.BusinessObjectID = targetItem.BusinessObjectID;
                        //delItem.MainSystem = targetItem.MainSystem;
                        //delItem.businessobject = targetItem.businessobject;
                        delItem.S3D_Note = targetItem.S3D_Note;

                        // 삭제 처리되는 System으로 변경                        
                        DelRows.Add(this.PedInfoToDataRow(delItem));
                    }
                }
                else
                {
                    if (ped.Hp == 0)
                    {
                        ped.RevisionType = RevisionDataType.UnChanged;
                    }
                    else
                    {
                        revisionDataType = RevisionDataType.Added;
                    }
                }

                ped.RevisionType = revisionDataType;
                row["RevisionType"] = revisionDataType;
            }

            foreach (var newRow in DelRows)
            {
                var ped = newRow["Entity"] as FdnPedInfo;
                ped.RevisionType = RevisionDataType.Deleted;
                newRow["RevisionType"] = RevisionDataType.Deleted;

                dtPedestal.Rows.Add(newRow);
            }

            DelRows.Clear();
            #endregion

            #region Tie-Girder
            foreach (DataRow row in this.dtTieGirder.Rows)
            {
                TieGirderInfo tie = row["Entity"] as TieGirderInfo;

                RevisionDataType revisionDataType = RevisionDataType.UnChanged;

                RevisionItemInfo item;
                if (ownFile)
                {
                    item = PedasSystems.FirstOrDefault(x => x.Note == tie.S3D_Note);
                }
                else
                {
                    // 앞의 File Key 를 제외하고 Item 을 찾아옴
                    item = PedasSystems.FirstOrDefault(x => x.NoteWithoutFileKey == tie.NoteWithoutFileKey);

                    if (PedasSystems.Count(x => x.NoteWithoutFileKey == tie.NoteWithoutFileKey) > 1)
                    {
                        // 다른 System 에 File Key가 다르고 Note 가 같은 Foundation 이 다수 존재
                        // 이런 경우에는 Converting 을 하지 않고 Remark에 알림 표시

                        row["Remark"] = error;
                        continue;
                    }

                    if (item != null)
                    {
                        // 2. Load 된 데이터와 비교
                        if (dtTieGirder.Select(string.Format("S3D_Note LIKE '%{0}'", item.NoteWithoutFileKey)).Length > 1)
                        {
                            row["Remark"] = error;
                            continue;
                        }
                    }
                }

                if (item != null)
                {
                    if (revisionDataType == RevisionDataType.UnChanged)
                    {
                        //SystemChild Path 검토
                        tie.SystemChildPath = string.Empty;

                        var PathString = PedasData.GetSP3D_CREATEPATH(EntityType.Tiegirder, false);

                        foreach (var txt in PathString)
                        {
                            tie.SystemChildPath += txt.Item1 + "◆";
                        }
                    }

                    TieGirderInfo targetItem = item.GraphicItem as TieGirderInfo;

                    targetTieGirder.Add(targetItem);

                    if (CompareRevision(tie, targetItem, this.TieGirderPropertyName))
                    {
                        revisionDataType = RevisionDataType.Modified;
                    }

                    if (CompareTGPoint(tie.TGPoints, targetItem.TGPoints))
                    {
                        revisionDataType = RevisionDataType.Modified;
                    }

                    // S3D에 같은 데이터가 있고, System 이 다르면 삭제 및 새로운 Row로 추가
                    if (tie.SystemID != targetItem.SystemID)
                    {
                        // 기존 데이터는 추가처리 (같은 row 삭제 Flag 로 추가)
                        revisionDataType = RevisionDataType.Added;

                        // 직접 변경 시, Back 했을때 영향감.
                        var delItem = new TieGirderInfo();
                        tie.CloneData(delItem);
                        delItem.SystemID = targetItem.SystemID;
                        delItem.SystemName = targetItem.SystemName;
                        delItem.BusinessObjectID = targetItem.BusinessObjectID;
                        //delItem.MainSystem = targetItem.MainSystem;
                        //delItem.businessobject = targetItem.businessobject;

                        // 삭제 처리되는 System으로 변경                        
                        DelRows.Add(this.TieGirderInfoToDataRow(delItem));
                    }
                }
                else
                {
                    revisionDataType = RevisionDataType.Added;
                }

                tie.RevisionType = revisionDataType;
                row["RevisionType"] = revisionDataType;
            }

            foreach (var newRow in DelRows)
            {
                var tie = newRow["Entity"] as TieGirderInfo;
                tie.RevisionType = RevisionDataType.Deleted;
                newRow["RevisionType"] = RevisionDataType.Deleted;

                dtTieGirder.Rows.Add(newRow);
            }
            #endregion

            // Check : 자신의 파일만 확인, False : 전체 Revision
            // Check 된 System 중에서 삭제할 데이터 계산
            foreach (DataRow sysRow in dtSystemPath.Select("Check = true"))
            {
                var sysID = sysRow["ID"].ToString();

                // 선택된 System 하위의 PEDAS-CVT Item 전체 조회
                var list = PedasSystems.Where(x => x.SystemID == sysID).ToList();

                foreach (var item in list)
                {
                    Action SetDeletedState = () =>
                    {
                        if (item.FoundationType == PedasFdnName.Footing)
                        {
                            var fdn = item.GraphicItem as FdnInfo;
                            DataRow[] rows;

                            if (ownFile)
                            {
                                rows = dtFooting.Select(string.Format("S3D_Note='{0}'", fdn.S3D_Note));
                            }
                            else
                            {
                                rows = dtFooting.Select(string.Format("NoteWithoutFileKey='{0}'", fdn.NoteWithoutFileKey));
                            }

                            if (rows.Length == 0)
                            {
                                fdn.RevisionType = RevisionDataType.Deleted;
                                dtFooting.Rows.Add(this.FdnInfoToDataRow(fdn));
                            }
                        }
                        else if (item.FoundationType == PedasFdnName.Pedestal)
                        {
                            var ped = item.GraphicItem as FdnPedInfo;
                            DataRow[] rows;

                            if (ownFile)
                            {
                                rows = dtPedestal.Select(string.Format("S3D_Note='{0}'", ped.S3D_Note));
                            }
                            else
                            {
                                rows = dtPedestal.Select(string.Format("NoteWithoutFileKey='{0}'", ped.NoteWithoutFileKey));
                            }

                            if (rows.Length == 0)
                            {
                                ped.RevisionType = RevisionDataType.Deleted;
                                dtPedestal.Rows.Add(this.PedInfoToDataRow((FdnPedInfo)item.GraphicItem));
                            }
                        }
                        else if (item.FoundationType == PedasFdnName.TieGirder)
                        {
                            var tg = item.GraphicItem as TieGirderInfo;
                            DataRow[] rows;

                            if (ownFile)
                            {
                                rows = dtTieGirder.Select(string.Format("S3D_Note='{0}'", tg.S3D_Note));
                            }
                            else
                            {
                                rows = dtTieGirder.Select(string.Format("NoteWithoutFileKey='{0}'", tg.NoteWithoutFileKey));
                            }

                            if (rows.Length == 0)
                            {
                                tg.RevisionType = RevisionDataType.Deleted;
                                dtTieGirder.Rows.Add(this.TieGirderInfoToDataRow((TieGirderInfo)item.GraphicItem));
                            }
                        }
                    };

                    if (ownFile)
                    {
                        // Check 상태면 자신의 Convert 파일만 검사해야함
                        if (item.FileKey == PedasData.ConvertKey)
                        {
                            SetDeletedState();
                        }
                    }
                    else
                    {
                        SetDeletedState();
                    }
                }
            }

            gvFoundation.ExpandAllGroups();
            gvPedestal.ExpandAllGroups();
            gvTieGirder.ExpandAllGroups();
        }

        private void SetRevisionType_PDMS()
        {
            this.GetSelectPedasObject();
            targetFooting.Clear();
            targetPedestal.Clear();
            targetTieGirder.Clear();
            targetDummy.Clear();

            bool ownFile = rgRevisionOption.SelectedIndex == 1;
            string error = "";

            error = "As a result of the identification, several foundations exist on the PDMS model with same name. So cannot be converted.";

            var DelRows = new List<DataRow>();

            DataTable dtDesignExplorer = new DataTable();
            dtDesignExplorer = XmlToDataTable(XmlDataFolderPath + @"\DesignExplorer_PedasItems.xml");
            var tempPedasSystems = PedasSystems.ToList();

            #region Compare iteration : Footing, Pedestal, TieGirder
            if (dtFooting.Rows.Count > 0)
            {
                foreach (DataRow row in this.dtFooting.Rows)
                {
                    Compare(row, "Footing");
                }
            }
            else
            {
                var drsysList = dtSystemPath.AsEnumerable().Where(r => (bool)r["Check"] == true).ToList();
                for (int i = tempPedasSystems.Count - 1; i >= 0; i--)
                {
                    if (drsysList.AsEnumerable().Where(r => (bool)r["Check"] == true && r["ID"].ToString() == tempPedasSystems[i].SystemID).Any())
                    {
                        FdnInfo tt = tempPedasSystems[i].GraphicItem as FdnInfo;
                        if(tt != null)
                        {
                            FdnInfo delItem1 = new FdnInfo();
                            tt.CloneData(delItem1);

                            delItem1.SystemID = tt.SystemID;
                            //delItem1.SystemName = tt.SystemName;                                           
                            delItem1.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == tt.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
                            delItem1.FoundationSystemID = tt.FoundationSystemID;
                            delItem1.BusinessObjectID = tt.BusinessObjectID;
                            delItem1.S3D_Note = tt.S3D_Note;
                            delItem1.RevisionType = RevisionDataType.Deleted;
                            // 삭제 처리되는 System으로 변경
                            DelRows.Add(this.FdnInfoToDataRow(delItem1));
                        }                      
                    }
                }
            }            
            
            SetDelRows(DelRows, "Footing");
            DelRows.Clear();

            if (dtPedestal.Rows.Count > 0)
            {
                foreach (DataRow row in this.dtPedestal.Rows)
                {
                    Compare(row, "Pedestal");
                }
            }
            else
            {
                var drsysList = dtSystemPath.AsEnumerable().Where(r => (bool)r["Check"] == true).ToList();
                for (int i = tempPedasSystems.Count - 1; i >= 0; i--)
                {
                    if (drsysList.AsEnumerable().Where(r => (bool)r["Check"] == true && r["ID"].ToString() == tempPedasSystems[i].SystemID).Any())
                    {
                        FdnPedInfo tt = tempPedasSystems[i].GraphicItem as FdnPedInfo;
                        if (tt != null)
                        {
                            FdnPedInfo delItem2 = new FdnPedInfo();
                            //ped_Entity.CloneData(delItem);
                            tt.CloneData(delItem2);

                            delItem2.IsS3D = tt.IsS3D; // S3D 상태로 바꿔주고 System Name 설정
                            delItem2.SystemID = tt.SystemID;
                            //delItem2.SystemName = tt.SystemName;
                            delItem2.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == tt.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
                            delItem2.BusinessObjectID = tt.BusinessObjectID;
                            delItem2.S3D_Note = tt.S3D_Note;
                            delItem2.RevisionType = RevisionDataType.Deleted;
                            // 삭제 처리되는 System으로 변경                        
                            DelRows.Add(this.PedInfoToDataRow(delItem2));
                        }                       
                    }
                }
            }
                      
            SetDelRows(DelRows, "Pedestal");
            DelRows.Clear();

            if(dtTieGirder.Rows.Count > 0)
            {
                foreach (DataRow row in this.dtTieGirder.Rows)
                {
                    Compare(row, "TieGirder");
                }
            }
            else
            {
                var drsysList = dtSystemPath.AsEnumerable().Where(r => (bool)r["Check"] == true).ToList();
                for (int i = tempPedasSystems.Count - 1; i >= 0; i--)
                {
                    if (drsysList.AsEnumerable().Where(r => (bool)r["Check"] == true && r["ID"].ToString() == tempPedasSystems[i].SystemID).Any())
                    {
                        TieGirderInfo tt = tempPedasSystems[i].GraphicItem as TieGirderInfo;
                        if (tt != null)
                        {
                            TieGirderInfo delItem3 = new TieGirderInfo();
                            tt.CloneData(delItem3);

                            delItem3.SystemID = tt.SystemID;
                            //delItem3.SystemName = tt.SystemName;
                            delItem3.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == tt.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
                            delItem3.BusinessObjectID = tt.BusinessObjectID;
                            delItem3.S3D_Note = tt.S3D_Note;
                            delItem3.RevisionType = RevisionDataType.Deleted;
                            // 삭제 처리되는 System으로 변경                        
                            DelRows.Add(this.TieGirderInfoToDataRow(delItem3));
                        }                      
                    }
                }                
            }

            SetDelRows(DelRows, "TieGirder");
            DelRows.Clear();
            #endregion

            void Compare(DataRow row, string type)
            {
                switch (type)
                {
                    case "Footing":
                        FdnInfo fdn_Entity = row["Entity"] as FdnInfo;
                        RevisionItemInfo revision_item = null;
                        if (CompareFile(ref revision_item, fdn_Entity, ref row, type))
                            PerformRevision_Footing(ref revision_item, ref fdn_Entity, ref row);
                        break;
                    case "Pedestal":
                        FdnPedInfo fdnPedInfo_Entity = row["Entity"] as FdnPedInfo;
                        RevisionItemInfo revision_item2 = null;
                        if (CompareFile(ref revision_item2, fdnPedInfo_Entity, ref row, type))
                            PerformRevision_Pedestal(ref revision_item2, ref fdnPedInfo_Entity, ref row);
                        break;
                    case "TieGirder":
                        TieGirderInfo tieGirderInfo_Entity = row["Entity"] as TieGirderInfo;
                        RevisionItemInfo revision_item3 = null;
                        if (CompareFile(ref revision_item3, tieGirderInfo_Entity, ref row, type))
                            PerformRevision_TieGirder(ref revision_item3, ref tieGirderInfo_Entity, ref row);
                        break;
                }
            }

            bool CompareFile(ref RevisionItemInfo revision_item, object Entity, ref DataRow row, string type)
            {
                string Entity_note = null;
                string Entity_note_without = null;
                string Entity_SystemID = null;
                DataTable dt = null;
                dynamic targetItem = null;

                switch (type)
                {
                    case "Footing":
                        Entity_note = ((FdnInfo)Entity).S3D_Note;
                        Entity_note_without = ((FdnInfo)Entity).NoteWithoutFileKey;
                        Entity_SystemID = ((FdnInfo)Entity).SystemID;
                        dt = dtFooting;
                        break;
                    case "Pedestal":
                        Entity_note = ((FdnPedInfo)Entity).S3D_Note;
                        Entity_note_without = ((FdnPedInfo)Entity).NoteWithoutFileKey;
                        Entity_SystemID = ((FdnPedInfo)Entity).SystemID;
                        dt = dtPedestal;
                        break;
                    case "TieGirder":
                        Entity_note = ((TieGirderInfo)Entity).S3D_Note;
                        Entity_note_without = ((TieGirderInfo)Entity).NoteWithoutFileKey;
                        Entity_SystemID = ((TieGirderInfo)Entity).SystemID;
                        dt = dtTieGirder;
                        break;
                }
                
                var drsysList = dtSystemPath.AsEnumerable().Where(r => (bool)r["Check"] == true).ToList();

                if (ownFile)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dynamic entity = dr["Entity"];
                       
                        for (int i = tempPedasSystems.Count - 1; i >= 0; i--)
                        {
                            if (drsysList.AsEnumerable().Where(r => (bool)r["Check"] == true && r["ID"].ToString() == tempPedasSystems[i].SystemID).Any())
                            {
                                try
                                {
                                    if (tempPedasSystems[i].SystemID == entity.SystemID && tempPedasSystems[i].Note == entity.S3D_Note)
                                    {
                                        tempPedasSystems.RemoveAt(i);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //do noting
                                }
                            }
                        }
                    }

                    revision_item = PedasSystems.ToList().FirstOrDefault(x => x.Note == Entity_note && x.SystemID == Entity_SystemID);

                    for (int i = tempPedasSystems.Count - 1; i >= 0; i--)
                    {
                        if (drsysList.AsEnumerable().Where(r => (bool)r["Check"] == true && r["ID"].ToString() == tempPedasSystems[i].SystemID).Any())
                        {
                            try
                            {
                                if (tempPedasSystems[i].SystemID == Entity_SystemID && tempPedasSystems[i].Note == Entity_note)
                                {
                                    tempPedasSystems.RemoveAt(i);
                                }
                                else
                                {
                                    #region 삭제 추가
                                    dynamic tt = null;
                                    if (tempPedasSystems[i] != null)
                                    {
                                        //단순 형변환
                                        switch (type)
                                        {
                                            case "Footing":
                                                tt = tempPedasSystems[i].GraphicItem as FdnInfo;
                                                FdnInfo delItem1 = new FdnInfo();
                                                tt.CloneData(delItem1);

                                                delItem1.SystemID = tt.SystemID;
                                                //delItem1.SystemName = tt.SystemName;                                           
                                                delItem1.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == tt.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
                                                delItem1.FoundationSystemID = tt.FoundationSystemID;
                                                delItem1.BusinessObjectID = tt.BusinessObjectID;
                                                delItem1.S3D_Note = tt.S3D_Note;

                                                // 삭제 처리되는 System으로 변경
                                                DelRows.Add(this.FdnInfoToDataRow(delItem1));
                                                break;
                                            case "Pedestal":
                                                tt = tempPedasSystems[i].GraphicItem as FdnPedInfo;
                                                FdnPedInfo delItem2 = new FdnPedInfo();
                                                //ped_Entity.CloneData(delItem);
                                                tt.CloneData(delItem2);

                                                delItem2.IsS3D = tt.IsS3D; // S3D 상태로 바꿔주고 System Name 설정
                                                delItem2.SystemID = tt.SystemID;
                                                //delItem2.SystemName = tt.SystemName;
                                                delItem2.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == tt.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
                                                delItem2.BusinessObjectID = tt.BusinessObjectID;
                                                delItem2.S3D_Note = tt.S3D_Note;

                                                // 삭제 처리되는 System으로 변경                        
                                                DelRows.Add(this.PedInfoToDataRow(delItem2));
                                                break;
                                            case "TieGirder":
                                                tt = tempPedasSystems[i].GraphicItem as TieGirderInfo;
                                                TieGirderInfo delItem3 = new TieGirderInfo();
                                                tt.CloneData(delItem3);

                                                delItem3.SystemID = tt.SystemID;
                                                //delItem3.SystemName = tt.SystemName;
                                                delItem3.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == tt.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
                                                delItem3.BusinessObjectID = tt.BusinessObjectID;
                                                delItem3.S3D_Note = tt.S3D_Note;
                                                // 삭제 처리되는 System으로 변경                        
                                                DelRows.Add(this.TieGirderInfoToDataRow(delItem3));
                                                break;
                                        }
                                    }
                                    tempPedasSystems.RemoveAt(i);
                                    #endregion
                                }
                            }
                            catch (Exception ex)
                            {
                                //do noting
                            }
                        }
                    }

                }
                else
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        dynamic entity = dr["Entity"];                                               
                        for (int i = tempPedasSystems.Count - 1; i >= 0; i--)
                        {
                            if (drsysList.AsEnumerable().Where(r => (bool)r["Check"] == true && r["ID"].ToString() == tempPedasSystems[i].SystemID).Any())
                            {
                                try
                                {
                                    if (tempPedasSystems[i].SystemID == entity.SystemID && tempPedasSystems[i].NoteWithoutFileKey == entity.NoteWithoutFileKey)
                                    {
                                        tempPedasSystems.RemoveAt(i);
                                    }                                  
                                }
                                catch (Exception ex)
                                {
                                    //do noting
                                }
                            }
                        }
                    }

                    revision_item = PedasSystems.ToList().FirstOrDefault(x => x.NoteWithoutFileKey == Entity_note_without && x.SystemID == Entity_SystemID);

                    for (int i = tempPedasSystems.Count - 1; i >= 0; i--)
                    {
                        if (drsysList.AsEnumerable().Where(r => (bool)r["Check"] == true && r["ID"].ToString() == tempPedasSystems[i].SystemID).Any())
                        {
                            try
                            {
                                if (tempPedasSystems[i].SystemID == Entity_SystemID && tempPedasSystems[i].NoteWithoutFileKey == Entity_note_without)
                                {
                                    tempPedasSystems.RemoveAt(i);
                                }
                                else
                                {
                                    #region 삭제 추가
                                    dynamic tt = null;
                                    if (tempPedasSystems[i] != null)
                                    {
                                        //단순 형변환
                                        switch (type)
                                        {
                                            case "Footing":
                                                tt = tempPedasSystems[i].GraphicItem as FdnInfo;
                                                FdnInfo delItem1 = new FdnInfo();
                                                tt.CloneData(delItem1);

                                                delItem1.SystemID = tt.SystemID;
                                                //delItem1.SystemName = tt.SystemName;                                           
                                                delItem1.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == tt.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
                                                delItem1.FoundationSystemID = tt.FoundationSystemID;
                                                delItem1.BusinessObjectID = tt.BusinessObjectID;
                                                delItem1.S3D_Note = tt.S3D_Note;

                                                // 삭제 처리되는 System으로 변경
                                                DelRows.Add(this.FdnInfoToDataRow(delItem1));
                                                break;
                                            case "Pedestal":
                                                tt = tempPedasSystems[i].GraphicItem as FdnPedInfo;
                                                FdnPedInfo delItem2 = new FdnPedInfo();
                                                //ped_Entity.CloneData(delItem);
                                                tt.CloneData(delItem2);

                                                delItem2.IsS3D = tt.IsS3D; // S3D 상태로 바꿔주고 System Name 설정
                                                delItem2.SystemID = tt.SystemID;
                                                //delItem2.SystemName = tt.SystemName;
                                                delItem2.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == tt.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
                                                delItem2.BusinessObjectID = tt.BusinessObjectID;
                                                delItem2.S3D_Note = tt.S3D_Note;

                                                // 삭제 처리되는 System으로 변경                        
                                                DelRows.Add(this.PedInfoToDataRow(delItem2));
                                                break;
                                            case "TieGirder":
                                                tt = tempPedasSystems[i].GraphicItem as TieGirderInfo;
                                                TieGirderInfo delItem3 = new TieGirderInfo();
                                                tt.CloneData(delItem3);

                                                delItem3.SystemID = tt.SystemID;
                                                //delItem3.SystemName = tt.SystemName;
                                                delItem3.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == tt.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
                                                delItem3.BusinessObjectID = tt.BusinessObjectID;
                                                delItem3.S3D_Note = tt.S3D_Note;
                                                // 삭제 처리되는 System으로 변경                        
                                                DelRows.Add(this.TieGirderInfoToDataRow(delItem3));
                                                break;
                                        }
                                    }
                                    tempPedasSystems.RemoveAt(i);
                                    #endregion
                                }
                            }
                            catch (Exception ex)
                            {
                                //do noting
                            }
                        }
                    }
                }

                //item 이 2개 이상일때
                if (PedasSystems.Count(x => x.NoteWithoutFileKey == Entity_note_without && x.SystemID == Entity_SystemID) > 1) // 1개 이상이 맞는지 재확인..
                {
                    // 다른 System 에 File Key가 다르고 Note 가 같은 Foundation 이 다수 존재
                    // 이런 경우에는 Converting 을 하지 않고 Remark에 알림 표시

                    row["Remark"] = error;

                    return false;
                }
                else if (revision_item != null)
                {
                    // 2. Load 된 데이터와 비교
                    //AND SystemID='{revision_item.SystemID}
                    if (dt.Select($"NoteWithoutFileKey = '{revision_item.NoteWithoutFileKey}' ").Length > 1)
                    {
                        row["Remark"] = error;
                        return false;
                    }
                }

                return true;
            }

            void PerformRevision_Footing(ref RevisionItemInfo revision_item, ref FdnInfo fdn_Entity, ref DataRow row)
            {                    
                RevisionDataType revisionDataType = RevisionDataType.UnChanged;
                if (revision_item != null)
                {
                    if (revisionDataType == RevisionDataType.UnChanged)
                    {
                        //SystemChild Path 검토
                        fdn_Entity.SystemChildPath = string.Empty;

                        var PathString = PedasData.GetSP3D_CREATEPATH(EntityType.Footing, false);

                        foreach (var txt in PathString)
                        {
                            fdn_Entity.SystemChildPath += txt.Item1 + "◆";
                        }
                    }

                    // 수정되었는지 데이터 비교 (ConvertFile 비교 아님)
                    FdnInfo targetItem = revision_item.GraphicItem as FdnInfo;

                    if (targetItem.FootShape == PEDAS_Shape.Rng && fdn_Entity.FootShape == PEDAS_Shape.Rng)
                    {
                        if (Math.Abs(fdn_Entity.Lx_mm - targetItem.Lx_mm) <= 5)
                        {
                            targetItem.Lx_mm = fdn_Entity.Lx_mm;
                        }
                    }

                    targetFooting.Add(targetItem);

                    if (fdn_Entity.FootShape == PEDAS_Shape.User)
                    {
                        if (CompareRevision(fdn_Entity, targetItem, this.FootingUserPropertyName))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }

                        var tempPoint = new List<FdnPoint>();
                        tempPoint.AddRange(fdn_Entity.FdnRevitPointList);

                        foreach (var op in fdn_Entity.FdnHolePointList)
                        {
                            foreach (var fdnHolePoint in op)
                            {
                                fdnHolePoint.GlobalZ = 0;
                            }
                            tempPoint.AddRange(op);
                        }

                        if (CompareFdnPoint(tempPoint, targetItem.FdnPointList))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }
                    }
                    else if (fdn_Entity.FootShape == PEDAS_Shape.Rng)
                    {
                        if (CompareRevision(fdn_Entity, targetItem, this.FootingRingPropertyName))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }

                        var tempPoint = new List<FdnPoint>();
                        tempPoint.AddRange(fdn_Entity.FdnRevitPointList);

                        foreach (var op in fdn_Entity.FdnHolePointList)
                        {
                            foreach (var fdnHolePoint in op)
                            {
                                fdnHolePoint.GlobalZ = 0;
                            }
                            tempPoint.AddRange(op);
                        }

                        if (CompareFdnPoint(tempPoint, targetItem.FdnPointList))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }
                    }
                    else
                    {
                        if (CompareRevision(fdn_Entity, targetItem, this.FootingPropertyName))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }

                        var tempPoint = new List<FdnPoint>();
                        tempPoint.AddRange(fdn_Entity.FdnRevitPointList);

                        foreach (var op in fdn_Entity.FdnHolePointList)
                        {
                            foreach (var fdnHolePoint in op)
                            {
                                fdnHolePoint.GlobalZ = 0;
                            }
                            tempPoint.AddRange(op);
                        }

                        //PDMS일때 모형에 상관없이 포인트좌표 비교
                        if (CompareFdnPoint(tempPoint, targetItem.FdnPointList))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }
                    }

                    if (revisionDataType == RevisionDataType.UnChanged)
                    {
                        if (fdn_Entity.isPileFdn)
                        {
                            string SystemChildPath = string.Empty;

                            var PathString = PedasData.GetSP3D_CREATEPATH(EntityType.Pile, false, targetItem.FDN_Name);

                            foreach (var txt in PathString)
                            {
                                SystemChildPath += txt.Item1 + "◆";
                            }
                            if (PedasData.ConvertProgramMode != ProgramMode.PDMS)
                            {
                                if (SystemChildPath != targetItem.PileList[0].SystemChildPath)
                                {
                                    revisionDataType = RevisionDataType.Modified;
                                }
                            }

                        }
                    }
                    //if (dtSystemPath.AsEnumerable().Where(r => (bool)r["Check"] == true && r[0].ToString() == targetItem.SystemID.ToString()).Any())
                    //{
                    //    // 같은 File 에서 Check 하는 것이라면
                    //    // S3D에 같은 데이터가 있고, System 이 다르면 삭제 및 새로운 Row로 추가
                    //    if (fdn_Entity.SystemID != targetItem.SystemID)
                    //    {
                    //        // 기존 데이터는 추가처리 (같은 row 삭제 Flag 로 추가)
                    //        revisionDataType = RevisionDataType.Added;

                    //        // fdn을 직접 바꿔버리면, Back 했을때 영향감.
                    //        var delItem = new FdnInfo();

                    //        //SMJO : 'ㅁ';;;
                    //        //fdn_Entity.CloneData(delItem);
                    //        targetItem.CloneData(delItem);

                    //        delItem.SystemID = targetItem.SystemID;
                    //        delItem.SystemName = targetItem.SystemName;
                    //        delItem.FoundationSystemID = targetItem.FoundationSystemID;
                    //        delItem.BusinessObjectID = targetItem.BusinessObjectID;
                    //        delItem.S3D_Note = targetItem.S3D_Note;

                    //        // 삭제 처리되는 System으로 변경
                    //        DelRows.Add(this.FdnInfoToDataRow(delItem));
                    //    }
                    //}
                }
                else
                {
                    // 추가
                    revisionDataType = RevisionDataType.Added;
                }

                fdn_Entity.RevisionType = revisionDataType;
                row["RevisionType"] = revisionDataType;
            }

            void PerformRevision_Pedestal(ref RevisionItemInfo revision_item, ref FdnPedInfo ped_Entity, ref DataRow row)
            {
                RevisionDataType revisionDataType = RevisionDataType.UnChanged;
                if (revision_item != null)
                {
                    // 수정되었는지 데이터 비교
                    FdnPedInfo targetItem = revision_item.GraphicItem as FdnPedInfo;

                    if (revisionDataType == RevisionDataType.UnChanged)
                    {
                        //SystemChild Path 검토
                        ped_Entity.SystemChildPath = string.Empty;

                        var PathString = PedasData.GetSP3D_CREATEPATH(EntityType.Pedestal, false);

                        foreach (var txt in PathString)
                        {
                            if (txt.Item2 == EntityType.Footing)
                            {
                                string temp_str = ped_Entity.FootingInfo.NoteWithoutFileKey;
                                var entity = PedasSystems.FirstOrDefault(x => x.NoteWithoutFileKey == temp_str);
                                FdnInfo footing = entity.GraphicItem as FdnInfo;

                                //if (footing.businessobject.GetType().ToString().Contains("Slab"))
                                if (footing.BusinessObjectTypeName.Contains("Slab"))
                                {

                                }
                                else
                                {
                                    ped_Entity.SystemChildPath += ped_Entity.FootingInfo.FDN_Name + "◆";
                                }
                            }
                            else
                                ped_Entity.SystemChildPath += txt.Item1 + "◆";
                        }
                    }

                    if (targetItem.Shape == PEDAS_Shape.Rng && ped_Entity.Shape == PEDAS_Shape.Rng)
                    {
                        if (Math.Abs(ped_Entity.dx - targetItem.dx) <= 5)
                        {
                            targetItem.dx = ped_Entity.dx;
                        }
                    }

                    targetPedestal.Add(targetItem);

                    if (ped_Entity.Shape == PEDAS_Shape.Rng)
                    {
                        if (CompareRevision(ped_Entity, targetItem, this.PedRingPropertyName))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }

                        var rows = dtFooting.Select(string.Format("FILE_ID = '{2}' AND FDN_ID ='{0}' AND RevisionType = '{1}'", ped_Entity.FDN_ID, RevisionDataType.Modified, ped_Entity.FILE_ID));

                        if (rows.Length > 0)
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }

                        var tempPoint = new List<FdnPoint>();
                        tempPoint.AddRange(ped_Entity.FdnPedPointList);

                        foreach (var fdnHolePoint in ped_Entity.FdnPedHolePointList)
                        {
                            fdnHolePoint.GlobalZ = 0;
                        }
                        tempPoint.AddRange(ped_Entity.FdnPedHolePointList);

                        if (CompareFdnPoint(tempPoint, targetItem.FdnPedPointList))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }
                    }
                    else
                    {
                        // Draw 할때 Grout 두께만큼 빼서 그렸음.
                        targetItem.Hp += targetItem.Grout_Thk;

                        if (CompareRevision(ped_Entity, targetItem, this.PedestalPropertyName))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }

                        var tempPoint = new List<FdnPoint>();
                        tempPoint.AddRange(ped_Entity.FdnPedPointList);

                        foreach (var fdnHolePoint in ped_Entity.FdnPedHolePointList)
                        {
                            fdnHolePoint.GlobalZ = 0;
                        }
                        tempPoint.AddRange(ped_Entity.FdnPedHolePointList);

                        if (CompareFdnPoint(tempPoint, targetItem.FdnPedPointList))
                        {
                            revisionDataType = RevisionDataType.Modified;
                        }

                        // 다시 빼준다.
                        targetItem.Hp -= targetItem.Grout_Thk;
                    }

                    //if (dtSystemPath.AsEnumerable().Where(r => (bool)r["Check"] == true && r[0].ToString() == targetItem.SystemID.ToString()).Any())
                    //{
                    //    // S3D에 같은 데이터가 있고, System 이 다르면 삭제 및 새로운 Row로 추가
                    //    if (ped_Entity.FootingInfo.SystemID != targetItem.SystemID)
                    //    {
                    //        // 기존 데이터는 추가처리 (같은 row 삭제 Flag 로 추가)
                    //        revisionDataType = RevisionDataType.Added;

                    //        // 직접 변경 시, Back 했을때 영향감.
                    //        var delItem = new FdnPedInfo();

                    //        //ped_Entity.CloneData(delItem);
                    //        targetItem.CloneData(delItem);


                    //        delItem.IsS3D = targetItem.IsS3D; // S3D 상태로 바꿔주고 System Name 설정
                    //        delItem.SystemID = targetItem.SystemID;
                    //        delItem.SystemName = targetItem.SystemName;
                    //        delItem.BusinessObjectID = targetItem.BusinessObjectID;
                    //        //delItem.MainSystem = targetItem.MainSystem;
                    //        //delItem.businessobject = targetItem.businessobject;
                    //        delItem.S3D_Note = targetItem.S3D_Note;

                    //        // 삭제 처리되는 System으로 변경                        
                    //        DelRows.Add(this.PedInfoToDataRow(delItem));
                    //    }
                    //}
                }
                else
                {
                    revisionDataType = RevisionDataType.Added;
                }

                ped_Entity.RevisionType = revisionDataType;
                row["RevisionType"] = revisionDataType;
            }

            void PerformRevision_TieGirder(ref RevisionItemInfo revision_item, ref TieGirderInfo tie_Entity, ref DataRow row)
            {
                RevisionDataType revisionDataType = RevisionDataType.UnChanged;
                if (revision_item != null)
                {
                    if (revisionDataType == RevisionDataType.UnChanged)
                    {
                        //SystemChild Path 검토
                        tie_Entity.SystemChildPath = string.Empty;

                        var PathString = PedasData.GetSP3D_CREATEPATH(EntityType.Tiegirder, false);

                        foreach (var txt in PathString)
                        {
                            tie_Entity.SystemChildPath += txt.Item1 + "◆";
                        }
                    }

                    TieGirderInfo targetItem = revision_item.GraphicItem as TieGirderInfo;

                    targetTieGirder.Add(targetItem);

                    if (CompareRevision(tie_Entity, targetItem, this.TieGirderPropertyName))
                    {
                        revisionDataType = RevisionDataType.Modified;
                    }

                    if (CompareTGPoint(tie_Entity.TGPoints, targetItem.TGPoints))
                    {
                        revisionDataType = RevisionDataType.Modified;
                    }
                    //if (dtSystemPath.AsEnumerable().Where(r => (bool)r["Check"] == true && r[0].ToString() == targetItem.SystemID.ToString()).Any())
                    //{
                    //    // S3D에 같은 데이터가 있고, System 이 다르면 삭제 및 새로운 Row로 추가
                    //    if (tie_Entity.SystemID != targetItem.SystemID)
                    //    {
                    //        // 기존 데이터는 추가처리 (같은 row 삭제 Flag 로 추가)
                    //        revisionDataType = RevisionDataType.Added;

                    //        // 직접 변경 시, Back 했을때 영향감.
                    //        var delItem = new TieGirderInfo();
                    //        //'ㅁ';;
                    //        //tie_Entity.CloneData(delItem);
                    //        targetItem.CloneData(delItem);

                    //        delItem.SystemID = targetItem.SystemID;
                    //        delItem.SystemName = targetItem.SystemName;
                    //        delItem.BusinessObjectID = targetItem.BusinessObjectID;
                    //        delItem.S3D_Note = targetItem.S3D_Note;
                    //        //delItem.MainSystem = targetItem.MainSystem;
                    //        //delItem.businessobject = targetItem.businessobject;

                    //        // 삭제 처리되는 System으로 변경                        
                    //        DelRows.Add(this.TieGirderInfoToDataRow(delItem));
                    //    }
                    //}
                }
                else
                {
                    revisionDataType = RevisionDataType.Added;
                }

                tie_Entity.RevisionType = revisionDataType;
                row["RevisionType"] = revisionDataType;
            }

            void SetDelRows(List<DataRow> DR, string type)
            {
                switch (type)
                {
                    case "Footing":
                        foreach (var newRow in DR)
                        {
                            var fdn = newRow["Entity"] as FdnInfo;
                            fdn.RevisionType = RevisionDataType.Deleted;
                            newRow["RevisionType"] = RevisionDataType.Deleted;

                            dtFooting.Rows.Add(newRow);
                        }
                        break;
                    case "Pedestal":
                        foreach (var newRow in DelRows)
                        {
                            var ped = newRow["Entity"] as FdnPedInfo;
                            ped.RevisionType = RevisionDataType.Deleted;
                            newRow["RevisionType"] = RevisionDataType.Deleted;

                            dtPedestal.Rows.Add(newRow);
                        }
                        break;
                    case "TieGirder":
                        foreach (var newRow in DelRows)
                        {
                            var tie = newRow["Entity"] as TieGirderInfo;
                            tie.RevisionType = RevisionDataType.Deleted;
                            newRow["RevisionType"] = RevisionDataType.Deleted;

                            dtTieGirder.Rows.Add(newRow);
                        }
                        break;
                }
            }

            DataTable XmlToDataTable(string file_path)
            {
                try
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(file_path);
                    return ds.Tables[0];
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            #region 참고
            //#region checkbox 'ㅁ'
            //// Check : 자신의 파일만 확인, False : 전체 Revision
            //// Check 된 System 중에서 삭제할 데이터 계산            
            //foreach (DataRow drSystemPath in dtSystemPath.Select("Check = true"))
            //{
            //    var systemPathID = drSystemPath["ID"].ToString();

            //    var list = PedasSystems.Where(x => x.SystemID == systemPathID).ToList();

            //    foreach (var item in list)
            //    {
            //        Action SetDeletedState = () =>
            //        {
            //            if (item.FoundationType == PedasFdnName.Footing)
            //            {
            //                var fdn = item.GraphicItem as FdnInfo;
            //                DataRow[] rows;

            //                if (ownFile)
            //                {
            //                    rows = dtFooting.Select(string.Format("S3D_Note='{0}'", fdn.S3D_Note));
            //                }
            //                else
            //                {
            //                    rows = dtFooting.Select(string.Format("NoteWithoutFileKey='{0}'", fdn.NoteWithoutFileKey));
            //                }

            //                //if (rows.Length == 0)
            //                //if (true)
            //                if (dtFooting.AsEnumerable().Where(r => r["NoteWithoutFileKey"].ToString() != fdn.NoteWithoutFileKey.ToString() && r["SystemID"].ToString() != fdn.SystemID.ToString()).Any())
            //                {
            //                    if (PedasData.ConvertProgramMode == ProgramMode.S3D)
            //                    {
            //                        fdn.RevisionType = RevisionDataType.Deleted;
            //                        dtFooting.Rows.Add(this.FdnInfoToDataRow(fdn));
            //                    }
            //                    else if (PedasData.ConvertProgramMode == ProgramMode.PDMS)
            //                    {
            //                        fdn.RevisionType = RevisionDataType.Deleted;
            //                        fdn.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == fdn.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
            //                        dtFooting.Rows.Add(this.FdnInfoToDataRow(fdn));
            //                    }
            //                }
            //            }
            //            else if (item.FoundationType == PedasFdnName.Pedestal)
            //            {
            //                var ped = item.GraphicItem as FdnPedInfo;
            //                DataRow[] rows;

            //                if (ownFile)
            //                {
            //                    rows = dtPedestal.Select(string.Format("S3D_Note='{0}'", ped.S3D_Note));
            //                }
            //                else
            //                {
            //                    rows = dtPedestal.Select(string.Format("NoteWithoutFileKey='{0}'", ped.NoteWithoutFileKey));
            //                }

            //                //if (rows.Length == 0)
            //                string systemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == ped.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
            //                dtPedestal.AsEnumerable().Select(r => r["NoteWithoutFileKey"]).FirstOrDefault();
            //                if (dtPedestal.AsEnumerable().Where(r => r["NoteWithoutFileKey"].ToString() != ped.NoteWithoutFileKey.ToString() && r["SystemName"].ToString() != systemName).Any())
            //                {
            //                    ped.RevisionType = RevisionDataType.Deleted;
            //                    ped.SystemName = systemName;
            //                    dtPedestal.Rows.Add(this.PedInfoToDataRow(ped));
            //                }
            //                //else if (dtPedestal.AsEnumerable().Where(r => r["SystemName"].ToString() != systemName).Any())
            //                //{
            //                //    ped.RevisionType = RevisionDataType.Deleted;
            //                //    ped.SystemName = systemName;
            //                //    dtPedestal.Rows.Add(this.PedInfoToDataRow(ped));
            //                //}
            //            }
            //            else if (item.FoundationType == PedasFdnName.TieGirder)
            //            {
            //                var tg = item.GraphicItem as TieGirderInfo;
            //                DataRow[] rows;

            //                if (ownFile)
            //                {
            //                    rows = dtTieGirder.Select(string.Format("S3D_Note='{0}'", tg.S3D_Note));
            //                }
            //                else
            //                {
            //                    rows = dtTieGirder.Select(string.Format("NoteWithoutFileKey='{0}'", tg.NoteWithoutFileKey));
            //                }

            //                //if (rows.Length == 0)
            //                if (dtTieGirder.AsEnumerable().Where(r => r["NoteWithoutFileKey"].ToString() != tg.NoteWithoutFileKey.ToString() && r["SystemID"].ToString() != tg.SystemID.ToString()).Any())
            //                {
            //                    if (PedasData.ConvertProgramMode == ProgramMode.S3D)
            //                    {
            //                        tg.RevisionType = RevisionDataType.Deleted;
            //                        dtTieGirder.Rows.Add(this.TieGirderInfoToDataRow(tg));
            //                    }
            //                    else if (PedasData.ConvertProgramMode == ProgramMode.PDMS)
            //                    {
            //                        tg.RevisionType = RevisionDataType.Deleted;
            //                        tg.SystemName = dtDesignExplorer.AsEnumerable().Where(r => r["REFNO"].ToString() == tg.SystemID).Select(r => r["NAME"]).FirstOrDefault().ToString();
            //                        dtTieGirder.Rows.Add(this.TieGirderInfoToDataRow(tg));
            //                    }
            //                }
            //            }
            //        };

            //        if (ownFile)
            //        {
            //            // Check 상태면 자신의 Convert 파일만 검사해야함
            //            if (item.FileKey == PedasData.ConvertKey)
            //            {
            //                SetDeletedState();
            //            }
            //        }
            //        else
            //        {
            //            SetDeletedState();
            //        }
            //    }
            //}
            //#endregion
            #endregion

            gvFoundation.ExpandAllGroups();
            gvPedestal.ExpandAllGroups();
            gvTieGirder.ExpandAllGroups();
        }

        private void CalcSummary(PedasFdnName pedasType)
        {
            dtSummary.Clear();

            if (pedasType == PedasFdnName.Footing)
            {
                dtSummary.Rows.Add(new object[] { RevisionDataType.UnChanged.ToString(), this.dtFooting.Select(string.Format("RevisionType = '{0}'", RevisionDataType.UnChanged.ToString())).Length });
                dtSummary.Rows.Add(new object[] { RevisionDataType.Added.ToString(), this.dtFooting.Select(string.Format("RevisionType = '{0}'", RevisionDataType.Added.ToString())).Length });
                dtSummary.Rows.Add(new object[] { RevisionDataType.Modified.ToString(), this.dtFooting.Select(string.Format("RevisionType = '{0}'", RevisionDataType.Modified.ToString())).Length });
                dtSummary.Rows.Add(new object[] { RevisionDataType.Deleted.ToString(), this.dtFooting.Select(string.Format("RevisionType = '{0}'", RevisionDataType.Deleted.ToString())).Length });
            }
            else if (pedasType == PedasFdnName.Pedestal)
            {
                dtSummary.Rows.Add(new object[] { RevisionDataType.UnChanged.ToString(), this.dtPedestal.Select(string.Format("RevisionType = '{0}'", RevisionDataType.UnChanged.ToString())).Length });
                dtSummary.Rows.Add(new object[] { RevisionDataType.Added.ToString(), this.dtPedestal.Select(string.Format("RevisionType = '{0}'", RevisionDataType.Added.ToString())).Length });
                dtSummary.Rows.Add(new object[] { RevisionDataType.Modified.ToString(), this.dtPedestal.Select(string.Format("RevisionType = '{0}'", RevisionDataType.Modified.ToString())).Length });
                dtSummary.Rows.Add(new object[] { RevisionDataType.Deleted.ToString(), this.dtPedestal.Select(string.Format("RevisionType = '{0}'", RevisionDataType.Deleted.ToString())).Length });
            }
            else if (pedasType == PedasFdnName.TieGirder)
            {
                dtSummary.Rows.Add(new object[] { RevisionDataType.UnChanged.ToString(), this.dtTieGirder.Select(string.Format("RevisionType = '{0}'", RevisionDataType.UnChanged.ToString())).Length });
                dtSummary.Rows.Add(new object[] { RevisionDataType.Added.ToString(), this.dtTieGirder.Select(string.Format("RevisionType = '{0}'", RevisionDataType.Added.ToString())).Length });
                dtSummary.Rows.Add(new object[] { RevisionDataType.Modified.ToString(), this.dtTieGirder.Select(string.Format("RevisionType = '{0}'", RevisionDataType.Modified.ToString())).Length });
                dtSummary.Rows.Add(new object[] { RevisionDataType.Deleted.ToString(), this.dtTieGirder.Select(string.Format("RevisionType = '{0}'", RevisionDataType.Deleted.ToString())).Length });
            }

            gcSummary.DataSource = dtSummary;
        }

        private bool CompareRevision(object org, object other, List<string> CheckProperties)
        {
            Type type_org = org.GetType();
            List<System.Reflection.PropertyInfo> properties_org = type_org.GetProperties().ToList();

            Type type_target = other.GetType();
            List<System.Reflection.PropertyInfo> properties_target = type_target.GetProperties().ToList();

            foreach (System.Reflection.PropertyInfo property in properties_org)
            {
                var find = properties_target.FirstOrDefault(p => p.Name == property.Name);

                if (org is FdnInfo)
                {
                    //Original이 Fdn이고 Pile이 없다면 아래 프로퍼티값들은 무시.
                    if (!((FdnInfo)org).isPileFdn)
                    {
                        if (find.Name == "PileLength_mm" || find.Name == "Pile_Diameter" || find.Name == "Pile_Shape"
                            || find.Name == "Pile_Ea" || find.Name == "Pile Point Group" || find.Name == "PilePrefix"
                            || find.Name == "PileNo")
                            continue;
                    }
                }

                //프로퍼티가 체크리스트에 있는것만 비교함.
                if (CheckProperties.Contains(find.Name))
                {
                    string val1 = property.GetValue(org, null) == null ? "" : property.GetValue(org, null).ToString();
                    string val2 = property.GetValue(other, null) == null ? "" : property.GetValue(other, null).ToString();

                    if (val1 != val2)
                        //같지 않으면 true.
                        return true;
                }
            }

            return false;
        }

        private bool CompareFdnPoint(List<FdnPoint> source, List<FdnPoint> target)
        {
            if (target == null) return true; // 이런경우는 Type 이 다름

            if (source.Count != target.Count) return true;

            var result = from i in source orderby i.GlobalX, i.GlobalY, i.GlobalZ ascending select i;
            var p1 = result.ToList();

            result = from i in target orderby i.X, i.Y, i.Z ascending select i;
            var p2 = result.ToList();

            for (int i = 0; i < p1.Count; i++)
            {
                if (p1[i].GlobalX != Math.Round(p2[i].X, 3) || p1[i].GlobalY != Math.Round(p2[i].Y, 3) || p1[i].GlobalZ != Math.Round(p2[i].Z, 3))
                {
                    //같지 않으면 true 리턴
                    return true;
                }
            }

            return false;
        }

        private bool CompareTGPoint(List<TieGirderPointInfo> source, List<TieGirderPointInfo> target)
        {
            if (source.Count != target.Count) return true;

            var result = from i in source orderby i.GlobalX, i.GlobalY, i.GlobalZ ascending select i;
            var p1 = result.ToList();

            result = from i in target orderby i.X, i.Y, i.Z ascending select i;
            var p2 = result.ToList();

            for (int i = 0; i < p1.Count; i++)
            {
                if (p1[i].GlobalX != Math.Round(p2[i].X, 3) || p1[i].GlobalY != Math.Round(p2[i].Y, 3) || p1[i].GlobalZ != Math.Round(p2[i].Z, 3))
                {
                    return true;
                }
            }

            return false;
        }

        private DataRow FdnInfoToDataRow(FdnInfo item)
        {
            DataRow row = dtFooting.NewRow();

            row["FDN_Name"] = item.FDN_Name;
            row["FootShape"] = item.FootShape;
            row["GlobalX"] = item.GlobalX;
            row["GlobalY"] = item.GlobalY;
            row["GlobalZ"] = item.GlobalZ;
            row["Hf_mm"] = item.Hf_mm;
            row["Hs_mm"] = item.Hs_mm;
            row["Lx_mm"] = item.Lx_mm;
            row["Ly_mm"] = item.Ly_mm;
            row["Pile_Ea"] = item.Pile_Ea;
            row["S3D_Note"] = item.S3D_Note;
            row["RevisionType"] = item.RevisionType.ToString();
            row["FDNTYPE_ID"] = item.FDNTYPE_ID;
            row["FILE_ID"] = item.FILE_ID;
            row["SystemID"] = item.SystemID;
            row["SystemName"] = item.SystemName;
            row["NoteWithoutFileKey"] = item.NoteWithoutFileKey;
            row["Entity"] = item;

            return row;
        }

        private DataRow PedInfoToDataRow(FdnPedInfo item)
        {
            DataRow row = dtPedestal.NewRow();

            row["PED_NAME"] = item.PED_NAME;
            row["Shape"] = item.Shape;
            row["GlobalX"] = item.GlobalX;
            row["GlobalY"] = item.GlobalY;
            row["GlobalZ"] = item.GlobalZ;
            row["Hp"] = item.Hp;
            row["Hpa"] = item.Hpa;
            row["dx"] = item.dx;
            row["dy"] = item.dy;
            row["Grout_Thk"] = item.Grout_Thk;
            row["S3D_Note"] = item.S3D_Note;
            row["RevisionType"] = item.RevisionType.ToString();
            row["PEDType_ID"] = item.PEDType_ID;
            row["FILE_ID"] = item.FILE_ID;
            row["SystemID"] = item.SystemID;
            row["SystemName"] = item.SystemName;
            row["NoteWithoutFileKey"] = item.NoteWithoutFileKey;
            row["Entity"] = item;

            return row;
        }

        private DataRow TieGirderInfoToDataRow(TieGirderInfo item)
        {
            DataRow row = dtTieGirder.NewRow();

            row["Name"] = item.Name;
            row["Start_Ped_Name"] = item.Start_Ped_Name;
            row["End_Ped_Name"] = item.End_Ped_Name;
            row["Width"] = item.Width;
            row["Height"] = item.Height;
            row["StartPointX"] = item.StartPointX;
            row["StartPointY"] = item.StartPointY;
            row["StartPointZ"] = item.StartPointZ;
            row["EndPointX"] = item.EndPointX;
            row["EndPointY"] = item.EndPointY;
            row["StartPointZ"] = item.StartPointZ;
            row["EndPointZ"] = item.EndPointZ;
            row["S3D_Note"] = item.S3D_Note;
            row["RevisionType"] = item.RevisionType.ToString();
            row["PARENTID"] = item.PARENTID;
            row["SystemID"] = item.SystemID;
            row["SystemName"] = item.SystemName;
            row["FILE_ID"] = item.FILE_ID;
            row["NoteWithoutFileKey"] = item.NoteWithoutFileKey;
            row["Entity"] = item;

            return row;
        }

        private DataRow DummyInfoToDataRow(DummyInfo item)
        {
            DataRow row = dtDummy.NewRow();

            row["DummyName"] = item.DummyName;
            row["S3D_Note"] = item.S3D_Note;
            row["RevisionType"] = item.RevisionType.ToString();
            row["SystemID"] = item.SystemID;
            row["SystemName"] = item.SystemName;
            row["FILE_ID"] = item.FILE_ID;
            row["NoteWithoutFileKey"] = item.NoteWithoutFileKey;

            row["Shape"] = item.Shape;
            row["GlobalX"] = item.GlobalX;
            row["GlobalY"] = item.GlobalY;
            row["GlobalZ"] = item.GlobalZ;
            row["PedWidth"] = item.PedWidth;
            row["PedHeight"] = item.PedHeight;
            row["PedLength"] = item.PedLength;
            row["FootingWidth"] = item.FootingWidth;
            row["FootingHeight"] = item.FootingHeight;
            row["FootingLength"] = item.FootingLength;
            row["Rotation"] = item.Rotation;
            row["Remark"] = item.REMARK;

            row["Entity"] = item;

            return row;
        }
        #endregion //end of 관련 메서드
        #endregion //end of 리비전 관련 컨트롤 초기화 (Next 버튼 클릭시)

    }
}
```