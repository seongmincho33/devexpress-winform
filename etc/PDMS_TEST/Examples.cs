using System;
using System.Collections.Generic;
using System.Text;
using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;
using Aveva.Pdms.Database;
using NOUN = Aveva.Pdms.Database.DbElementTypeInstance;
using Ps = Aveva.Pdms.Database.DbPseudoAttribute;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using Aveva.Pdms.Shared;
using Aveva.PDMS.Database.Filters;

namespace PDMS_TEST
{
    public class Examples : Aveva.ApplicationFramework.IAddin
    {
        private static ServiceManager sServiceManager;
        private static CommandManager sCommandManager;        
        private static CommandBarManager sCommandBarManager;
        
        private DataTable DesignExplorerTable { get; set; }

        void Aveva.ApplicationFramework.IAddin.Start(ServiceManager serviceManager)
        {
            this.SetServices(serviceManager);
            //this.SetServices2(serviceManager);
        }       

        private void SetServices(ServiceManager serviceManager)
        {
            sServiceManager = serviceManager;
            sCommandManager = (CommandManager)sServiceManager.GetService(typeof(CommandManager));
            sCommandBarManager = (CommandBarManager)sServiceManager.GetService(typeof(CommandBarManager));

            //버튼 : 기존있던것
            sCommandManager.Commands.Add(new ExampleCommand());
            CommandBar myToolBar = sCommandBarManager.CommandBars.AddCommandBar("TeklaConvert");
            ButtonTool tool = sCommandBarManager.RootTools.AddButtonTool("TeklaConvert", "Import Model From Tekla", null, "TeklaConvert");
            tool.Tooltip = "Huen";
            tool.ToolClick += new EventHandler(tool_ToolClick);


            //버튼 : 프로퍼티 확인
            sCommandManager.Commands.Add(new ExampleCommand002());
            CommandBar myToolBar002 = sCommandBarManager.CommandBars.AddCommandBar("TEST_SMJO");
            ButtonTool tool002 = sCommandBarManager.RootTools.AddButtonTool("TEST_SMJO", "TEST_BUTTON", null, "TEST_SMJO");
            tool002.Tooltip = "Huen";
            tool002.ToolClick += new EventHandler(tool_ToolClick002);


            //버튼 : 트리리스트 확인
            sCommandManager.Commands.Add(new ExampleCommand003());
            CommandBar myToolBar003 = sCommandBarManager.CommandBars.AddCommandBar("TEST_SMJO_TREE");
            ButtonTool tool003 = sCommandBarManager.RootTools.AddButtonTool("TEST_SMJO_TREE", "TEST_SMJO_TREE", null, "TEST_SMJO_TREE");
            tool003.Tooltip = "Huen";
            tool003.ToolClick += new EventHandler(tool_ToolClick003);


            myToolBar003.Tools.AddTool("TEST_SMJO_TREE");
            myToolBar002.Tools.AddTool("TEST_SMJO");
            myToolBar.Tools.AddTool("TeklaConvert");
        }

        private void SetServices2(ServiceManager serviceManager)
        {
            sServiceManager = serviceManager;            
            sCommandManager = (CommandManager)sServiceManager.GetService(typeof(CommandManager));
            sCommandBarManager = (CommandBarManager)sServiceManager.GetService(typeof(CommandBarManager));

            //버튼 : 기존있던것
            sCommandManager.Commands.Add(new ExampleCommand());
            CommandBar myToolBar = sCommandBarManager.CommandBars.AddCommandBar("TeklaConvert");
            ButtonTool tool = sCommandBarManager.RootTools.AddButtonTool("TeklaConvert", "Import Model From Tekla", null, "TeklaConvert");
            tool.Tooltip = "Huen";
            tool.ToolClick += new EventHandler(tool_ToolClick);


            //버튼 : 프로퍼티 확인
            sCommandManager.Commands.Add(new ExampleCommand002());
            CommandBar myToolBar002 = sCommandBarManager.CommandBars.AddCommandBar("TEST_SMJO");
            ButtonTool tool002 = sCommandBarManager.RootTools.AddButtonTool("TEST_SMJO", "TEST_BUTTON", null, "TEST_SMJO");
            tool002.Tooltip = "Huen";
            tool002.ToolClick += new EventHandler(tool_ToolClick002);


            //버튼 : 트리리스트 확인
            sCommandManager.Commands.Add(new ExampleCommand003());
            CommandBar myToolBar003 = sCommandBarManager.CommandBars.AddCommandBar("TEST_SMJO_TREE");
            ButtonTool tool003 = sCommandBarManager.RootTools.AddButtonTool("TEST_SMJO_TREE", "TEST_SMJO_TREE", null, "TEST_SMJO_TREE");
            tool003.Tooltip = "Huen";
            tool003.ToolClick += new EventHandler(tool_ToolClick003);


            myToolBar003.Tools.AddTool("TEST_SMJO_TREE");
            myToolBar002.Tools.AddTool("TEST_SMJO");
            myToolBar.Tools.AddTool("TeklaConvert");
        }

        #region 버튼 이벤트
        void tool_ToolClick(object sender, EventArgs e)
        {            
            //당분간 사용안함

            //this.ShowWorldElementPropertyStatus();
            //TeklaToPDMSConverterForm frm = new TeklaToPDMSConverterForm();
            //frm.();
            //System.Diagnostics.Process program = System.Diagnostics.Process.Start(@"D:\Huen_SVN\PDMS_Convert\PDMS_Convert17\Converter.GetTelkaComponent\bin\Debug\Tekla2PdsConverter.exe");
        }

        void tool_ToolClick002(object sender, EventArgs e)
        {           
            //this.CreateSeongminElement(); // 엘리먼트 생성
            this.ShowDbElementInside(Aveva.Pdms.Shared.CurrentElement.Element); // 속성값 보기
        }

        void tool_ToolClick003(object sender, EventArgs e)
        {
            this.ShowDesignExplorerToTreeList(); // treelist 폼 보기            
        }
        #endregion

        #region 2022-04 제작함 

        #region 메서드 묶음
        private void ShowDbElementInside(DbElement element)
        {            
            this.ShowElementType(element);
            this.ShowElementMember(element);
            this.ShowAttribute(element);
            this.DifferenceBetweenPropertiesAndAttributes(element);
        }

        private void ShowDesignExplorerToTreeList()
        {
            //DesignExplorerTable에 컬럼을 셋팅합니다.
            this.SetDesignExplorerTableColumns();

            //DesignExplorerTable에 navigate된 데이터를 넣습니다. (Navigate방식이기 때문에 시간이 오래걸림)
            //this.NavigateDbElement(DbElement.GetElement("/*"));

            // DesignExplorerTable에 Structural 필터링된 DbElement들을 넣습니다.
            this.SetElementsToDesignExplorerTable();

            //폼 생성자에게 데이터 테이블 넘기고 띄우기
            frmDesignTreeList frmTree = new frmDesignTreeList(DesignExplorerTable);
            frmTree.Show();
        }
        #endregion

        /// <summary>
        /// DesignExplorerTable 의 테이블 컬럼 설정
        /// </summary>
        private void SetDesignExplorerTableColumns()
        {
            try
            {
                this.DesignExplorerTable = new DataTable();
                this.DesignExplorerTable.TableName = "data";
                this.DesignExplorerTable.Columns.Add(new DataColumn("NAME", typeof(string)));
                this.DesignExplorerTable.Columns.Add(new DataColumn("OWNER", typeof(string)));
                this.DesignExplorerTable.Columns.Add(new DataColumn("DBELEMENT", typeof(DbElement)));

                //DesignExplorerTable.Rows.Add("test_name", null, Aveva.Pdms.Shared.CurrentElement.Element);
                //DesignExplorerTable.Rows.Add("test_name_1", "test_name", Aveva.Pdms.Shared.CurrentElement.Element);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Design Explorer 데이터 조회
        /// </summary>
        private void SetElementsToDesignExplorerTable()
        {
            #region Design Explorer Filter

            Aveva.PDMS.Database.Filters.EquipmentFilter equipmentFilter = new EquipmentFilter();
            Aveva.PDMS.Database.Filters.HangerFilter hangerFilter = new HangerFilter();
            Aveva.PDMS.Database.Filters.PipingFilter pipingFilter = new PipingFilter();
            Aveva.PDMS.Database.Filters.StructuralFilter structuralFilter = new StructuralFilter();

            // Filter 사용 시
            //DBElementCollection dbCollection = new DBElementCollection(DbElement.GetElement("/*"), structuralFilter);
            #endregion

            // Design Explorer 최상위 Root 조회 및 모든 레벨 노드까지 전체 탐색 (DBElementCollection)
            DBElementCollection dbCollection = new DBElementCollection(DbElement.GetElement("/*"));

            //테이블에 NAME: *, OWNER: null, DBELEMENT: * 이 필요함(TreeListView의 루트노트가 필요하기 때문)
            this.DesignExplorerTable.Rows.Add(DbElement.GetElement("/*").ToString(), null, DbElement.GetElement("/*"));

            foreach (DbElement dbElement in dbCollection)
            {
                try
                {
                    //MessageBox.Show(
                    //"OWNER : " + dbElement.Owner.ToString()
                    //+ "\n\n"
                    //+ "NAME : " + dbElement.ToString()
                    //+ "\n\n"
                    //+ "GetElementType : " + dbElement.GetElementType().ToString());
                    this.DesignExplorerTable.Rows.Add(dbElement.ToString(), dbElement.Owner.ToString(), dbElement);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }       

        /// <summary>
        /// DbElement를 생성합니다. 
        /// </summary>
        private void CreateSeongminElement()
        {            
            try
            {
                //월드
                DbElement world = DbElement.GetElement("/*");

                //월드-사이트
                DbElement site = world.Create(1, NOUN.SITE);
                site.SetAttribute(DbAttributeInstance.NAME, "/SEONGMIN-TEST-SITE");

                //월드-사이트-존
                DbElement zone = site.Create(1, NOUN.ZONE);
                zone.SetAttribute(DbAttributeInstance.NAME, "/SEONGMIN-TEST-ZONE");

                //월드-사이트-존-equipment
                DbElement equipment = zone.Create(1, NOUN.EQUIPMENT);
                equipment.SetAttribute(DbAttributeInstance.NAME, "/SEONGMIN-TEST-EQUIPMENT");

                //월드-사이트-존-equipment-box
                DbElement box = equipment.Create(1, NOUN.BOX);
                box.SetAttribute(DbAttributeInstance.NAME, "/SEONGMIN-TEST-BOX");

                //월드-사이트-존-structure
                DbElement structure = zone.Create(1, NOUN.STRUCTURE);
                structure.SetAttribute(DbAttributeInstance.NAME, "/SEONGMIN-TEST-STRUCTURE");
               
                //월드-사이트-존-structure-frmwork
                DbElement frmwork = structure.Create(1, NOUN.FRMWORK);
                frmwork.SetAttribute(DbAttributeInstance.NAME, "/SEONGMIN-TEST-FRMWORK");

                //월드-사이트-존-structure-frmwork-sctn
                DbElement sctn = frmwork.Create(1, NOUN.SCTN);
                sctn.SetAttribute(DbAttributeInstance.NAME, "/SEONGMIN-TEST-SCTN");

                //DbElement myNewPLT = frmwork.Create(1, NOUN.PLATE);

                //포지션
                Aveva.Pdms.Geometry.Position start = Aveva.Pdms.Geometry.Position.Create();
                start.X = 12491;
                start.Y = 122801;
                start.Z = 1151;

                Aveva.Pdms.Geometry.Position end = Aveva.Pdms.Geometry.Position.Create();
                end.X = 12491;
                end.Y = 123801;
                end.Z = 1151;

                Aveva.Pdms.Geometry.Position zeroPoint = Aveva.Pdms.Geometry.Position.Create();
                zeroPoint.X = 0;
                zeroPoint.Y = 0;
                zeroPoint.Z = 0;
               

                //sctn
                sctn.SetAttribute(DbAttribute.GetDbAttribute("POSS"), start);
                sctn.SetAttribute(DbAttribute.GetDbAttribute("POSE"), end);
                
                //박스
                box.SetAttribute(DbAttributeInstance.POS, zeroPoint);
                box.SetAttribute(DbAttributeInstance.XLEN, 15.0d);
                box.SetAttribute(DbAttributeInstance.YLEN, 10.0d);
                box.SetAttribute(DbAttributeInstance.ZLEN, 13.0d);



                //DbElement spre = DbElement.GetElement("/BS-SPEC/203x133x30kg/m");

                //myNewSCTN.SetAttribute(DbAttribute.GetDbAttribute("SPRE"), spre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(GetPropertyString(DbAttributeInstance.XLEN));
            }
            
        }

        /// <summary>
        /// 이 클래스의 데이터테이블 프로퍼티안에 Design Explorer 트리 구조를 넣어줍니다. 속도 느림...
        /// </summary>
        /// <param name="child_element"></param>
        private void NavigateDbElement(DbElement child_element)
        {
            
            try
            {
                if(child_element.ToString() == "Null Element")
                {                   
                    return;
                }
                while (child_element.ToString() != "Null Element")
                {                   
                    NavigateDbElement(child_element.FirstMember());
                    //MessageBox.Show("OWNER : " + child_element.Owner.ToString() + "NAME : " + child_element.ToString());                    
                    if(child_element.Owner.ToString() == "Null Element")
                    {                                                
                        this.DesignExplorerTable.Rows.Add(child_element.ToString(), null, child_element);                                               
                    }
                    else
                    {
                        //MessageBox.Show(
                        //    "GetElementType : " + child_element.GetElementType().ToString() 
                        //    + "\n\n" 
                        //    + "DbElementTypeInstance : " + DbElementTypeInstance.STRUCTURE
                        //    + "\n\n"
                        //    + "OWNER : " + child_element.Owner.ToString() 
                        //    + "\n\n" 
                        //    + "NAME : " + child_element.ToString());
                                                
                        this.DesignExplorerTable.Rows.Add(child_element.ToString(), child_element.Owner.ToString(), child_element);                                                    
                    }
                                                  
                    if(child_element.Next().ToString() != "Null Element"
                        && (child_element.ToString() != child_element.Next().ToString()))
                    {
                        child_element = child_element.Next();
                    }              
                    else
                    {
                        break;
                    }
                    
                }
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 선택한 DbElement의 element.GetElementType()를 확인합니다. 
        /// </summary>
        private void ShowElementType(DbElement element)
        {
            //DbElement element = Aveva.Pdms.Shared.CurrentElement.Element;
            if(MessageBox.Show("클릭한 DbElement의 타입을 보시겠습니까?", "element.GetElementType()", MessageBoxButtons.OKCancel) != DialogResult.Cancel)
            {
                MessageBox.Show(
                    "!!현재 클릭한 DbElement의 타입!! = " 
                    + element.GetElementType() 
                    + "\n\n" 
                    + "아래는 현재 클릭한 DbElement의 프로퍼티 값들 입니다.(GetAttributes()아님주의)"
                    + this.GetPropertyString(element));                
            }            
        }

        /// <summary>
        /// 선택한 DbElement의 멤버와 멤버들의 dbElement.GetElementType()값
        /// </summary>
        private void ShowElementMember(DbElement element)
        {
            //DbElement element = Aveva.Pdms.Shared.CurrentElement.Element;
            if (MessageBox.Show("클릭한 DbElement의 멤버를 보시겠습니까?", "element.Members() 와 members.GetElementType()", MessageBoxButtons.OKCancel) != DialogResult.Cancel)
            {                
                var message = new StringBuilder();
                foreach (DbElement dbElement in element.Members())
                {
                    message.AppendLine(
                        "멤버 : "
                        + dbElement.ToString()
                        + "\n"
                        + "타입 : "
                        + dbElement.GetElementType().ToString()
                        + "\n");
                }

                MessageBox.Show("!!현재 클릭한 엘리먼트의 멤버!! " + "\n\n" + message.ToString());
            }
        }

        /// <summary>
        /// 루트 월드의 사용가능성을 체크합니다. 필요없음..
        /// </summary>
        public void ShowWorldElementPropertyStatus()
        {
            DbElement element = DbElement.GetElement("/*");
            MessageBox.Show("World Element가 null인가? : " + element.IsNull.ToString());
            MessageBox.Show("World Element가 Valid한가? : " + element.IsValid.ToString());
            MessageBox.Show("World Element는 무슨타입인가? : " + element.GetElementType().ToString());            

            MessageBox.Show(this.GetPropertyString(element));
        }       

        /// <summary>
        /// 현재 select한 DbElement의 GetAttributes()내역을 보여줍니다. 또한 GetAttributes로 불러온 것들의 프로퍼티를 확인합니다.
        /// </summary>
        private void ShowAttribute(DbElement hsElement)
        {
            try
            {
                if (MessageBox.Show("선택한 DbElement의 속성을 보시겠습니까?.", "element.GetAttributes()", MessageBoxButtons.OKCancel) != DialogResult.Cancel)
                {
                    //DbElement hsElement = Aveva.Pdms.Shared.CurrentElement.Element;
                    var message = new StringBuilder();

                    message.AppendLine("아래는 선택한 DbElement의 element.GetAttributes()값들입니다. ");

                    foreach (DbAttribute dbAttribute in hsElement.GetAttributes())
                    {
                        message.AppendLine(dbAttribute.ToString());
                    }

                    message.AppendLine("\n");

                    message.AppendLine("아래는 선택한 DbElement의 element.GetAttributes()값들의 파라미터값들입니다.");

                    message.AppendLine("\n");

                    foreach (DbAttribute dbAttribute in hsElement.GetAttributes())
                    {
                        //DbAttribute dbAttributeValue = null;

                        //if (hsElement.GetValidAttribute(dbAttribute, ref dbAttributeValue))
                        //{
                        //    message.AppendLine(
                        //    dbAttribute.ToString()
                        //    + " : "
                        //    + dbAttributeValue.ToString()
                        //    + " \n "
                        //    + this.GetPropertyString(dbAttribute));
                        //}
                        message.AppendLine(
                            dbAttribute.ToString()
                            + " \n "
                            + this.GetPropertyString(dbAttribute));
                    }
                    MessageBox.Show(message.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// element.GetAttributes() 과 GetPropertyString(element)의 차이점
        /// </summary>
        private void DifferenceBetweenPropertiesAndAttributes(DbElement hsElement)
        {
            try
            {
                if (MessageBox.Show("선택한 DbElement의 속성과 프로퍼티의 차이점을 보시겠습니까?.", "element.GetAttributes()", MessageBoxButtons.OKCancel) != DialogResult.Cancel)
                {
                    //DbElement hsElement = Aveva.Pdms.Shared.CurrentElement.Element;
                    var message = new StringBuilder();

                    message.AppendLine("아래는 선택한 DbElement의 element.GetAttributes()값들입니다. ");

                    foreach (DbAttribute dbAttribute in hsElement.GetAttributes())
                    {
                        message.AppendLine(dbAttribute.ToString());
                    }
                    message.AppendLine("\n");
                    message.AppendLine("아래는 선택한 DbElement의 GetPropertyString(element)값들입니다. ");
                    message.AppendLine("\n");

                    message.AppendLine(GetPropertyString(hsElement));

                    MessageBox.Show(message.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        /// <summary>
        /// 단순하게, 객체의 프로퍼티값들의 Value를 찾아서 StringBuilder로 만듭니다.
        /// </summary>
        /// <param name="component">아무 객체를 넣어주세요</param>
        /// <returns>"프로퍼티_이름 = 값 \n" 형식의 string을 반환합니다. </returns>
        private string GetPropertyString(object component)
        {
            var message = new StringBuilder();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(component))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(component);
                message.AppendLine(String.Format("{0} = {1}", name, value));
            }

            return message.ToString();
        }

        #endregion

        #region 참고 메서드
        //조회
        private void PDMS_Search()
        {
            //Zone을 가져옴
            DbElement zone = DbElement.GetElement("/PIPES");

            //다음 Zone
            DbElement temp = zone.Next();

            //이전 Zone
            temp = zone.Previous;

            //Zone의 상위
            temp = zone.Owner;

            //Zone의 첫번째 Member
            temp = zone.FirstMember();

            //Zone의 마지막 Member
            temp = zone.LastMember();


            DbElement temp2 = temp.Next(NOUN.EQUIPMENT);
            DbElement temp3 = temp.Previous;
        }

        //attribute
        private void PDMS_Attribute()
        {
            ////사용할 Type
            //NOUN.PANEL;NOUN.SCTN;

            DbElement zone = DbElement.GetElement("/PIPES");

            DbElement element;

            //Members
            foreach (var item in zone.Members())
            {
                element = item;

                //GetAttribute (One)
                DbAttribute uda = item.GetAttribute(DbAttribute.GetDbAttribute("DESC"));

                //Attributes
                foreach (DbAttribute ATT in item.GetAttributes())
                {

                    try
                    {
                        //GetAttribute
                        item.GetString(ATT);

                        ////Attribute가져오면서 에러나는경우가 있음
                        //System.Windows.Forms.MessageBox.Show("Att : " + item.GetString(ATT));
                    }
                    catch { }

                    if (ATT.Name == "DESC")
                    {
                        //SetAttribute
                        item.SetAttribute(ATT, "NCY");

                        //System.Windows.Forms.MessageBox.Show("Members " + item.GetString(ATT));
                        //ATT.GetString();
                    }
                }
                break;
            }
        }

        private void ViewClassMap()
        {
            //List<TeklaPdsClassMap> classDbMaps = Bll.TeklaPdsClassMap.GetTeklaPdsClassMaps();

            ////ConverterEntities ent = new ConverterEntities();

            ////if (ent.ClassMaps == null)
            ////    System.Windows.Forms.MessageBox.Show("Test");

            //DataTable dtMapping = new DataTable();
            //dtMapping.Columns.Add("Name");
            //dtMapping.Columns.Add("AssemblyPrefix");
            //dtMapping.Columns.Add("TeklaClass");
            //dtMapping.Columns.Add("PdsClass");

            //foreach (var item in classDbMaps)
            //{
            //    dtMapping.Rows.Add(new object[] { item.Name, item.AssemblyPrefix, item.TeklaClass, item.PdsClass});

            //}

            //dtMapping.WriteXml("D:\\PDMS_MAPPING.xml");
        }

        private void Spre_Member(DbElement del, DataTable dtSpre)
        {
            DbElement[] tmp = del.Members();
            if (tmp != null && tmp.Length > 0)
            {
                foreach (DbElement item in del.Members())
                {
                    Spre_Member(item, dtSpre);
                    DataRow dr = dtSpre.NewRow();
                    dr["Spre"] = item.ToString();
                    dtSpre.Rows.InsertAt(dr, 0);
                }
            }
        }

        private DataTable PDMS_SPRE()
        {
            DataTable dtSpre = new DataTable();
            dtSpre.TableName = "PDMS_SPRE";
            dtSpre.Columns.Add("Spre");

            DbElement spre1 = DbElement.GetElement("/MASTER/STCLCATA");

            for (; ; )
            {
                if (spre1.Previous.ToString() == "Null Element")
                    break;
                else
                    spre1 = spre1.Previous;
            }

            for (; ; )
            {
                Spre_Member(spre1, dtSpre);

                DataRow dr = dtSpre.NewRow();
                dr["Spre"] = spre1.ToString();
                dtSpre.Rows.InsertAt(dr, 0);

                spre1 = spre1.Next();

                if (spre1.ToString() == "Null Element")
                    break;
            }

            return dtSpre;

            dtSpre.WriteXml("D:\\PDMS_SPRE.xml");
        }

        private DataTable PDMS_TYPE()
        {
            DataTable dtattribute = new DataTable();

            dtattribute.Columns.Add("NAME");
            dtattribute.Columns.Add("SNAME");
            dtattribute.Columns.Add("DESC");
            //dtattribute.Columns.Add("BASETYPE");
            //dtattribute.Columns.Add("HARDTYPE");
            //dtattribute.Columns.Add("PRIMARY");
            //dtattribute.Columns.Add("PRIMARYLIST");
            //dtattribute.Columns.Add("IsPseudo");
            //dtattribute.Columns.Add("IsSynonym");
            //dtattribute.Columns.Add("IsValid");
            //dtattribute.Columns.Add("IsWorld");
            //dtattribute.Columns.Add("ValidConnections");
            //dtattribute.Columns.Add("Visible");

            dtattribute.TableName = "PDMS Type";

            foreach (DbElementType item in DbElementType.GetAllElementTypes())
            {
                if (item.Name != "" && item.Name != null)
                    dtattribute.Rows.Add(new object[] { item.Name, item.ShortName, item.Description });

                //dtattribute.Rows.Add(new object[] { item.Name, item.ShortName, item.Description, item.BaseType, item.HardType,
                //    item.IsPrimary, item.IsPrimaryList, item.IsPseudo, item.IsSynonym, item.IsValid, item.IsWorld,
                //    item.ValidConnections, item.Visible });
            }

            return dtattribute;

            dtattribute.WriteXml("D:\\PDMS_TYPE.xml");
        }

        private void Create_Element()
        {
            DbElement zone = DbElement.GetElement("/SAMPLE-ADMIN");

            DbElement myNewSTRU = zone.Create(1, NOUN.STRUCTURE);

            DbElement myNewFRM = myNewSTRU.Create(1, NOUN.FRMWORK);

            DbElement myNewSCTN = myNewFRM.Create(1, NOUN.SCTN);

            DbElement myNewPLT = myNewFRM.Create(1, NOUN.PLATE);

            Aveva.Pdms.Geometry.Position start = Aveva.Pdms.Geometry.Position.Create();
            start.X = 12491;
            start.Y = 122801;
            start.Z = 1151;

            Aveva.Pdms.Geometry.Position end = Aveva.Pdms.Geometry.Position.Create();
            end.X = 12491;
            end.Y = 123801;
            end.Z = 1151;

            myNewSCTN.SetAttribute(DbAttribute.GetDbAttribute("POSS"), start);

            myNewSCTN.SetAttribute(DbAttribute.GetDbAttribute("POSE"), end);

            DbElement spre = DbElement.GetElement("/BS-SPEC/203x133x30kg/m");

            myNewSCTN.SetAttribute(DbAttribute.GetDbAttribute("SPRE"), spre);

        }

        private DataTable PDMS_GetAttribute()
        {
            DataTable dtattribute = new DataTable();

            dtattribute.Columns.Add("CATEGORY");
            dtattribute.Columns.Add("NAME");
            dtattribute.Columns.Add("DESC");
            dtattribute.Columns.Add("ARRAY");
            dtattribute.Columns.Add("PSEUDO");
            dtattribute.Columns.Add("UDA");
            dtattribute.Columns.Add("QUERYTEXT");
            dtattribute.Columns.Add("TYPE");
            dtattribute.Columns.Add("UNITS");

            dtattribute.TableName = "PDMS ATTRIBUTE";

            DbElement zone = DbElement.GetElement("/NCY");

            DbElement myNewSTRU = zone.Create(1, NOUN.STRUCTURE);

            DbElement myNewFRM = myNewSTRU.Create(1, NOUN.FRMWORK);

            DbElement myNewSCTN = myNewFRM.Create(1, NOUN.SCTN);

            foreach (var item in myNewSCTN.GetAttributes())
            {

                dtattribute.Rows.Add(new object[] { item.Category, item.ShortName, item.Description, item.IsArray.ToString(), item.IsPseudo.ToString(), item.IsUDA.ToString(), item.QueryText, item.Type, item.Units });

                if (item.IsArray)
                {
                    foreach (DbAttributeField item3 in Enum.GetValues(typeof(DbAttributeField)))
                    {
                        foreach (var item2 in item.GetAsStringArray(item3))
                        {
                            dtattribute.Rows.Add(new object[] { "", item3.ToString(), item2.ToString() });
                        }
                    }
                }
            }

            return dtattribute;

            dtattribute.WriteXml("d:\\a.xml");
        }       

        ////attribute추가 안됨
        //static public void RegisterDelegate()
        //{
        //    DbAttribute uda = DbAttribute.GetDbAttribute(":TTT");

        //    //Ps.GetStringDelegate dele = new Ps.GetStringDelegate(VolumeCalculation);

        //    //Ps.AddGetStringAttribute(uda, dele);
        //}

        ////attribute추가 안됨
        //static private string VolumeCalculation(DbElement ele, DbAttribute att, int qualifier)
        //{
        //    //double x = ele.GetString(;
        //    //double y = 3;
        //    //double z = 3;


        //    return "aa";
        //}

        void Aveva.ApplicationFramework.IAddin.Stop()
        {

        }

        String Aveva.ApplicationFramework.IAddin.Name
        {
            get
            {
                return "Import model From Tekla";
            }
        }
        String Aveva.ApplicationFramework.IAddin.Description
        {
            get
            {
                return "SAMPLE";
            }
        }
        #endregion       
    }
}
