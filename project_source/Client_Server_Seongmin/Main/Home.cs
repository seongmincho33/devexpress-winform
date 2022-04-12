using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UC_Library;

namespace Main
{
    public partial class Home : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private UC_DBConnection uC_DBConnection;
        private UC.UC_Member uC_Main;
        private UC.UC_User uC_User;

        public Home()
        {
            InitializeComponent();
            this.CreateUC();
            this.SetControls();
        }

        private void CreateUC()
        {
            this.uC_DBConnection = new UC_DBConnection();
            this.uC_Main = new UC.UC_Member(uC_DBConnection);
            this.uC_User = new UC.UC_User(uC_DBConnection);
        }

        private void SetControls()
        {
            SetMenuButtons();
            
            

            void SetMenuButtons()
            {
                //화면
                this.accordion_Form.Elements["accordion_MainForm"].Click += FormMenu_Click;
                void FormMenu_Click(object sender, EventArgs e)
                {                    
                    if (((DevExpress.XtraBars.Navigation.AccordionControlElementBase)sender).Name == "accordion_MainForm")
                    {
                        this.fluentDesignFormContainer1.Controls.Clear();
                        uC_Main.Dock = DockStyle.Fill;
                        this.fluentDesignFormContainer1.Controls.Add(uC_Main);
                    }                  
                }

                //셋팅
                this.accordion_Settings.Elements["accordion_Settings_DB"].Click += SettingMenu_Click;
                this.accordion_Settings.Elements["accordion_Settings_User"].Click += SettingMenu_Click;
                void SettingMenu_Click(object sender, EventArgs e)
                {                    
                    if (((DevExpress.XtraBars.Navigation.AccordionControlElementBase)sender).Name == "accordion_Settings_DB")
                    {
                        this.fluentDesignFormContainer1.Controls.Clear();
                        uC_DBConnection.Dock = DockStyle.Fill;
                        this.fluentDesignFormContainer1.Controls.Add(uC_DBConnection);
                    }
                    else if(((DevExpress.XtraBars.Navigation.AccordionControlElementBase)sender).Name == "accordion_Settings_User")
                    {
                        this.fluentDesignFormContainer1.Controls.Clear();
                        uC_User.Dock = DockStyle.Fill;
                        this.fluentDesignFormContainer1.Controls.Add(uC_User);
                    }               
                }
            }
        }
    }
}
