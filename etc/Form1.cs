using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aveva.Pdms.Database;
using Aveva.Pdms.Standalone;

namespace PDMS_CONNECTION_TEST
{
    public partial class Form1 : Form
    {
        public static string setupContent = @"
set PROCESSOR_IDENTIFIER=AMD64 Family 25 Model 80 Stepping 0, AuthenticAMD
set JAVA_TOOL_OPTIONS = -Djava.net.preferIPv4Stack = true
set BOCEXE = C:\Program Files(x86)\AVEVA\Everything3D2.10\EXE\
set cadc_plot_dir = C:\PROGRA ~2\AVEVA\EVERYT ~1.10\plot.exe\
set BOCWORK = C:\Users\HEC\AppData\Local\Temp\
set pdms_installed_dir = C:\Program Files(x86)\AVEVA\Everything3D2.10\
set PUBLIC = C:\Users\Public
  set SDNFDFLTS=C:\Users\Public\Documents\AVEVA\EVERYT ~1\Data2.10\DFLTS\SDNF\dflts\;C:\Users\Public\Documents\AVEVA\EVERYT ~1\Data2.10\DFLTS\
set ALLUSERSPROFILE = C:\ProgramData
  set SDNFUI=C:\PROGRA ~2\AVEVA\EVERYT ~1.10\PMLUI\
set AVEVA_DESIGN_ACAD = 2006
set monexe = C:\Program Files(x86)\AVEVA\Everything3D2.10\
set executable = mon
set LOCALAPPDATA = C:\Users\HEC\AppData\Local
  set SDNFUSER=C:\Users\Public\Documents\AVEVA\USERDATA\
set ProgramData = C:\ProgramData
  set AVEVA_SHORTPATHS=false
set TSTEXP = C:\Users\Public\Documents\AVEVA\EVERYT ~1\Data2.10\DFLTS\SDNF\\test\results\export
   set AVEVA_DESIGN_ABA_REPORT=C:\
set USERDNSDOMAIN = HEC.CO.KR
set AVEVA_DESIGN_REP_DIR = C:\PROGRA ~2\AVEVA\EVERYT ~1.10\PMLUI\reports\
set pdmsdflts = C:\Users\Public\Documents\AVEVA\EVERYT ~1\Data2.10\DFLTS\
set BOCUI = C:\PROGRA ~2\AVEVA\EVERYT ~1.10\PMLUI\
set PDMS_CONSOLE_STDOUT = C:\Users\HEC\AppData\Local\Temp\pdm2487.tmp
  set pdms_acad_path=C:\program files\AutoCAD 2006;C:\Program Files\Common Files\Autodesk Shared
set ProgramW6432 = C:\Program Files
set mdsplots = C:\PROGRA ~2\AVEVA\EVERYT ~1.10\PMLUI\des\mds\plot\
set AVEVA_APPLICATION_DATA = C:\Users\Public\Documents\AVEVA\Everything3D\Data2.10\
set AVEVA_DESIGN_PLOTS = C:\PROGRA ~2\AVEVA\EVERYT ~1.10\PMLUI\plots\
set AVEVA_DESIGN_EXE = C:\Program Files(x86)\AVEVA\Everything3D2.10\;
set BOCDATA = C:\Users\Public\Documents\AVEVA\Everything3D\Data2.10\bocad\
set AVEVA_PRODUCT = 3D
 set SystemDrive=C:
set cplmdsexe = C:\ThirdParty\WH4\WH4win.exe
  set ZWCADPATH=C:\PROGRA ~1\ZWSOFT\ZWCAD2 ~1\ZWCAD.EXE
    set OS=Windows_NT
    set PROCESSOR_ARCHITECTURE=x86
    set found=C:\Program Files(x86)\AVEVA\Everything3D2.10\
set PROCESSOR_ARCHITEW6432 = AMD64
set PATHEXT =.COM;.EXE;.BAT;.CMD;.VBS;.VBE;.JS;.JSE;.WSF;.WSH;.MSC
 set licadmdsexe=C:\ThirdParty\Licad\LICAD\LICAD_MDS.exe
 set nextpart=C:\Program Files(x86)\AVEVA\Everything3D2.10\
set pslmdsexe = C:\ThirdParty\PSL\PSLDesigner\PSDesigner.exe
  set pdmswk=C:\Users\HEC\AppData\Local\Temp\
set TSTIMP = C:\Users\Public\Documents\AVEVA\EVERYT ~1\Data2.10\DFLTS\SDNF\\test\results\import
   set CommonProgramW6432=C:\Program Files\Common Files
set ProgramFiles(x86)=C:\Program Files(x86)
set ACAD_VERSION = 2006
set common_tools = C:\
set SDNFDATA = C:\Users\Public\Documents\AVEVA\EVERYT ~1\Data2.10\DFLTS\SDNF\
set USERNAME = 1844085
set SYUNIQ = X3630
set AVEVA_DESIGN_ABA_LOG = C:\
set args = graphics
set ProgramFiles = C:\Program Files (x86)
set AVEVA_DESIGN_LOG=C:\
set CommonProgramFiles=C:\Program Files (x86)\Common Files
set PROMPT=$P$G
set BOCDFLTS=C:\Users\Public\Documents\AVEVA\Everything3D\Data2.10\bocad\dflts\;C:\Users\Public\Documents\AVEVA\EVERYT~1\Data2.10\DFLTS\
set OneDrive=C:\Users\HEC\OneDrive
set BOCMAIN=C:\Program Files (x86)\AVEVA\Everything3D2.10\
set PSModulePath=C:\Program Files\WindowsPowerShell\Modules;C:\Windows\system32\WindowsPowerShell\v1.0\Modules
set PDMSUI=C:\PROGRA~2\AVEVA\EVERYT~1.10\PMLUI\
set pdms_acad=2006
set USERDOMAIN=HEC
set AVEVA_DESIGN_ABA_BATCH=C:\
set LOGONSERVER=\\HC-AS-A2
set USERDOMAIN_ROAMINGPROFILE=HEC
set COMPUTERNAME=1844085N01
set AVEVA_DESIGN_ACAD_PATH=C:\program files\AutoCAD 2006;C:\Program Files\Common Files\Autodesk Shared
set PDMSEXE=C:\Program Files (x86)\AVEVA\Everything3D2.10\;
set CommonProgramFiles(x86)=C:\Program Files (x86)\Common Files
set pdms_console_window=ACTIVE
set AVEVA_DESIGN_INSTALLED_DIR=C:\Program Files (x86)\AVEVA\Everything3D2.10\
set SystemRoot=C:\Windows
set ComSpec=C:\Windows\system32\cmd.exe
set VS140COMNTOOLS=C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\
set AVEVA_DESIGN_USER=C:\Users\Public\Documents\AVEVA\USERDATA\
set PROCESSOR_LEVEL=25
set PMLUI=C:\PROGRA~2\AVEVA\EVERYT~1.10\PMLUI\
set HOMEDRIVE=C:
set AVEVA_DESIGN_DFLTS=C:\Users\Public\Documents\AVEVA\EVERYT~1\Data2.10\DFLTS\
set USERPROFILE=C:\Users\HEC
set pdmsplots=C:\PROGRA~2\AVEVA\EVERYT~1.10\PMLUI\plots\
set AVEVA_DESIGN_ABA_HYPER=C:\
set savepath=C:\Win32App\INGR\SPLM\bin;C:\Windows\Softcamp\SDK;C:\Windows\Softcamp\SDS\x64;C:\Windows\Softcamp\SDS;C:\Windows\system32;C:\Windows;C:\Windows\System32\Wbem;C:\Windows\System32\WindowsPowerShell\v1.0\;C:\Windows\System32\OpenSSH\;C:\Users\HEC\.dnx\bin;C:\Program Files\Microsoft DNX\Dnvm\;C:\Program Files (x86)\Microsoft VS Code\bin;C:\Program Files (x86)\Microsoft SQL Server\150\DTS\Binn\;C:\Program Files\Azure Data Studio\bin;C:\Program Files (x86)\Common Files\Aveva Shared\Plot\;C:\Users\HEC\AppData\Local\Microsoft\WindowsApps;C:\Program Files\Bandizip\;C:\Program Files\Azure Data Studio\bin
set projects_dir=C:\Users\Public\Documents\AVEVA\Projects\E3D2.1\
set MDS_SUPPORT_CONFIG_LOCATION=C:\Program Files (x86)\AVEVA\Everything3D2.10\
set cadc_ipcdir=C:\Program Files (x86)\AVEVA\Everything3D2.10\null\
set Path=C:\Program Files (x86)\AVEVA\Everything3D2.10\;\autodraftACAD;C:\program files\AutoCAD 2006;C:\Program Files\Common Files\Autodesk Shared;C:\Win32App\INGR\SPLM\bin;C:\Windows\Softcamp\SDK;C:\Windows\Softcamp\SDS\x64;C:\Windows\Softcamp\SDS;C:\Windows\system32;C:\Windows;C:\Windows\System32\Wbem;C:\Windows\System32\WindowsPowerShell\v1.0\;C:\Windows\System32\OpenSSH\;C:\Users\HEC\.dnx\bin;C:\Program Files\Microsoft DNX\Dnvm\;C:\Program Files (x86)\Microsoft VS Code\bin;C:\Program Files (x86)\Microsoft SQL Server\150\DTS\Binn\;C:\Program Files\Azure Data Studio\bin;C:\Program Files (x86)\Common Files\Aveva Shared\Plot\;C:\Users\HEC\AppData\Local\Microsoft\WindowsApps;C:\Program Files\Bandizip\;C:\Program Files\Azure Data Studio\bin;C:\Program Files (x86)\AVEVA\Everything3D2.10\;C:\Program Files (x86)\AVEVA\Everything3D2.10\dars;C:\Program Files (x86)\AVEVA\Everything3D2.10\autodraftACAD;C:\Program Files (x86)\AVEVA\Everything3D2.10\PMLUI\plots\
set ACAD=C:\Program Files (x86)\AVEVA\Everything3D2.10\;\AutoDraftFonts
set SDNFMAIN=C:\Program Files (x86)\AVEVA\Everything3D2.10\
set SESSIONNAME=Console
set windir=C:\Windows
set APPDATA=C:\Users\HEC\AppData\Roaming
set PROCESSOR_REVISION=5000
set PDMSLOG=C:\
set NUMBER_OF_PROCESSORS=12
set sampsi=C:\
set SDNFBLOCKMAPS=C:\Users\Public\Documents\AVEVA\EVERYT~1\Data2.10\DFLTS\\BlockMaps\
set pdmshelpdir=C:\PROGRA~2\AVEVA\EVERYT~1.10\Documentation\
set DriverData=C:\Windows\System32\Drivers\DriverData
set AVEVA_DESIGN_ABA_PLOT=C:\
set ADSK_CLM_WPAD_PROXY_CHECK=FALSE
set SDNFWORK=C:\Users\HEC\AppData\Local\Temp\
set HOMEPATH=\Users\HEC
set AVEVA_DESIGN_WORK=C:\Users\HEC\AppData\Local\Temp\
set LHD000ID=LHD
set pdmsrepdir=C:\PROGRA~2\AVEVA\EVERYT~1.10\PMLUI\reports\
set plotcadc=C:\Program Files (x86)\AVEVA\Everything3D2.10\plot.exe
set SDNFEXE=C:\Program Files (x86)\AVEVA\Everything3D2.10\
set TEMP=C:\Users\HEC\AppData\Local\Temp
set BOCUSER=C:\Users\Public\Documents\AVEVA\USERDATA\
set pdmsuser=C:\Users\Public\Documents\AVEVA\USERDATA\
set TMP=C:\Users\HEC\AppData\Local\Temp
set AVEVA_DESIGN_CONSOLE_WINDOW=ACTIVE
set AVEVA_DESIGN_ABA_DATA=C:\
set AVEVA_DESIGN_HELP_DIR=C:\PROGRA~2\AVEVA\EVERYT~1.10\Documentation\
";
        public Form1()
        {
            InitializeComponent();
            this.btnServerStart.Click += BtnServerStart_Click;
            this.btnDataCall.Click += BtnDataCall_Click;
            SetEnvironment();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string val = args.Name;

            throw new NotImplementedException();
        }

        private void BtnServerStart_Click(object sender, EventArgs e)
        {            
            this.ServerStart("Sample", "SYSTEM", "XXXXXX", "SAMPLE");
        }        

        public void ServerStart(string DB_SOURCE, string DB_USER_ID, string DB_PASSWORD, string DB_NAME)
        {
            try
            {
                PdmsStandalone.Start(0);

                MessageBox.Show("1");
                if (!PdmsStandalone.Start())
                {
                    //MessageBox.Show("exiterror 1");
                    //PdmsStandalone.ExitError("Failed to Start - Start");
                }                    

                MessageBox.Show("2");

                
                if (!PdmsStandalone.Open(DB_SOURCE, DB_USER_ID, DB_PASSWORD, DB_NAME))
                {
                    //MessageBox.Show("exiterror 1");
                    //PdmsStandalone.ExitError("Failed to Start - Open");
                }
                
                MessageBox.Show("3");

                this.richTextBox1.Text = "연결 성공";
            }
            catch(Exception ex)
            {
                PdmsStandalone.ExitError("Failed to Start - Exception");
                PdmsStandalone.Close();
                PdmsStandalone.Finish();
            }
        }

        public static void SetEnvironment()
        {
            var evarsContents = setupContent.Split(Environment.NewLine.ToCharArray());

            foreach(var evarsContent in evarsContents)
            {
                var evarsContentValues = evarsContent.Split('=');
                if (evarsContentValues.Length < 2) continue;

                var variableName = evarsContentValues[0].Substring(evarsContentValues[0].IndexOf("set", StringComparison.Ordinal) + 4);

                Environment.SetEnvironmentVariable(variableName.Trim(), evarsContentValues[1]);
            }
        }

        private void BtnDataCall_Click(object sender, EventArgs e)
        {
            //this.NavigateDbElement(DbElement.GetElement("/*"));
            this.richTextBox1.Text = "월드 가져오기 : " + DbElement.GetElement("/*").ToString();
        }

        private void NavigateDbElement(DbElement child_element)
        {

            try
            {
                if (child_element.ToString() == "Null Element")
                {
                    return;
                }
                while (child_element.ToString() != "Null Element")
                {
                    NavigateDbElement(child_element.FirstMember());
                    //MessageBox.Show("OWNER : " + child_element.Owner.ToString() + "NAME : " + child_element.ToString());
                    this.richTextBox1.AppendText("OWNER : " + child_element.Owner.ToString() + "NAME : " + child_element.ToString());
                    if (child_element.Owner.ToString() == "Null Element")
                    {
                        //this.DesignExplorerTable.Rows.Add(child_element.ToString(), null, child_element);
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
                        this.richTextBox1.AppendText("GetElementType : " + child_element.GetElementType().ToString()
                            + "\n\n"
                            + "DbElementTypeInstance : " + DbElementTypeInstance.STRUCTURE
                            + "\n\n"
                            + "OWNER : " + child_element.Owner.ToString()
                            + "\n\n"
                            + "NAME : " + child_element.ToString());
                        //this.DesignExplorerTable.Rows.Add(child_element.ToString(), child_element.Owner.ToString(), child_element);
                    }

                    if (child_element.Next().ToString() != "Null Element"
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
                //MessageBox.Show(ex.ToString());
                this.richTextBox1.Text = ex.ToString();
            }
        }

        private void btnServerStart_Click_1(object sender, EventArgs e)
        {

        }
    }
}
