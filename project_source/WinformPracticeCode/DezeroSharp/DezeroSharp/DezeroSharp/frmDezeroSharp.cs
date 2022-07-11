using DezeroMath;
using MathNet.Numerics.LinearAlgebra;
using Plot3D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Plot3D.ColorSchema;
using static Plot3D.Graph3D;

namespace DezeroSharp
{
    public partial class frmDezeroSharp : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmDezeroSharp()
        {
            InitializeComponent();                                  
            this.Load += Main_Load;            
        }       

        private void Main_Load(object sender, EventArgs e)
        {
            this.SetControls();
           
            this.StartMatrixPractice();
           
        }

        #region 수학관련
        private void StartMatrixPractice()
        {
            var x = new Variable<Matrix<double>>(Matrix<double>.Build.Dense(1, 1, 0.5));
            MathFactory mf = new MathFactory();
            var a = mf.Square(x);
            var b = mf.exp(a);
            var y = mf.Square(b);


            //var A = new Square<Matrix<double>>();
            //var B = new Exp<Matrix<double>>();
            //var C = new Square<Matrix<double>>();

            //var a = A.Call(x);
            //var b = B.Call(a);
            //var y = C.Call(b);


            //y.Gradient = 1;

            //b.Gradient = C.BackWard(y.Gradient);
            //a.Gradient = B.BackWard(b.Gradient);
            //x.Gradient = A.BackWard(a.Gradient);



            //C = (Square<double>)y.Creator;
            //b = C.Input;
            //b.Gradient = C.Backward(y.Gradient);

            //B = (Exp<double>)b.Creator;
            //a = B.Input;
            //a.Gradient = B.Backward(b.Gradient);

            //A = (Square<double>)a.Creator;
            //x = A.Input;
            //x.Gradient = A.Backward(a.Gradient);


            //y.Gradient = 1;



            y.Backward();

            this.memoEditCenter.Text = x.Gradient.ToString();

        }
        #endregion

        #region 컨트롤 셋팅
        private void SetControls()
        {
            this.SetBarControls();
            this.SetGraph3DControlsEvents();
        }       

        private void SetGraph3DControlsEvents()
        {
            //콤보박스
            this.comboDataSrc.SelectedIndexChanged += comboDataSrc_SelectedIndexChanged;
            this.comboColors.SelectedIndexChanged += comboColors_SelectedIndexChanged;
            this.comboRaster.SelectedIndexChanged += comboRaster_SelectedIndexChanged;
            
            //버튼
            this.btnReset.Click += btnReset_Click;
            this.btnScreenshot.Click += btnScreenshot_Click;
        }

        private void SetBarControls()
        {
            this.bSISaveMenuButton.ItemClick += BSISaveMenuButton_ItemClick;
            this.bBISaveScreenShot.ItemClick += BBISaveScreenShot_ItemClick;
            this.bBIGraphPanel.ItemClick += BBIGraphPanel_ItemClick;
            this.bBIMemoPanel.ItemClick += BBIMemoPanel_ItemClick;
            this.bBIGraphControlPanel.ItemClick += BBIGraphControlPanel_ItemClick;
            this.bBISmallSetting.ItemClick += BBISmallSetting_ItemClick;
        }

        #endregion

        #region 컨트롤 이벤트
        private void BBIGraphPanel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dockManager1.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Fill, this.dPGraphic);
            this.dPGraphic.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        private void BBIMemoPanel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dockManager1.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Bottom, this.dPMemo);
            this.dPMemo.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        private void BBIGraphControlPanel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dockManager1.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Right, this.dPGraphController);
            this.dPGraphController.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        private void BBISmallSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dockManager1.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Left, this.dPSmallSetting);
            this.dPSmallSetting.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        private void BSISaveMenuButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void BBISaveScreenShot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog i_Dlg = new SaveFileDialog();
            i_Dlg.Title = "Save as PNG image";
            i_Dlg.Filter = "PNG Image|*.png";
            i_Dlg.DefaultExt = ".png";

            if (DialogResult.Cancel == i_Dlg.ShowDialog(this))
                return;

            Bitmap i_Bitmap = graph3D.GetScreenshot();
            try
            {
                i_Bitmap.Save(i_Dlg.FileName, ImageFormat.Png);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 그래프 관련
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // This is optional. If you don't want to use Trackbars leave this away.
            graph3D.AssignTrackBars(trackRho, trackTheta, trackPhi);

            comboRaster.Properties.Sorted = false;
            foreach (eRaster e_Raster in Enum.GetValues(typeof(eRaster)))
            {
                comboRaster.Properties.Items.Add(e_Raster);
            }
            comboRaster.SelectedIndex = (int)eRaster.Labels;

            comboColors.Properties.Sorted = false;
            foreach (eSchema e_Schema in Enum.GetValues(typeof(eSchema)))
            {
                comboColors.Properties.Items.Add(e_Schema);
            }
            comboColors.SelectedIndex = (int)eSchema.Rainbow1;

            comboDataSrc.SelectedIndex = 0; // set "Callback"
        }

        private void comboDataSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            graph3D.AxisX_Legend = null;
            graph3D.AxisY_Legend = null;
            graph3D.AxisZ_Legend = null;

            switch (comboDataSrc.SelectedIndex)
            {
                case 0: SetCallback(); break;
                case 1: SetFormula(); break;
                case 2: SetSurface(); break;
                case 3: SetScatterPlot(false); break;
                case 4: SetScatterPlot(true); break;
                case 5: SetValentine(); break;
            }

            lblInfo.Text = "Points: " + graph3D.TotalPoints;
        }

        private void comboColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color[] c_Colors = ColorSchema.GetSchema((eSchema)comboColors.SelectedIndex);
            graph3D.SetColorScheme(c_Colors, 3);
        }

        private void comboRaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            graph3D.Raster = (eRaster)comboRaster.SelectedIndex;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            graph3D.SetCoefficients(1350, 70, 230);
        }

        private void btnScreenshot_Click(object sender, EventArgs e)
        {
            SaveFileDialog i_Dlg = new SaveFileDialog();
            i_Dlg.Title = "Save as PNG image";
            i_Dlg.Filter = "PNG Image|*.png";
            i_Dlg.DefaultExt = ".png";

            if (DialogResult.Cancel == i_Dlg.ShowDialog(this))
                return;

            Bitmap i_Bitmap = graph3D.GetScreenshot();
            try
            {
                i_Bitmap.Save(i_Dlg.FileName, ImageFormat.Png);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================================================================================

        /// <summary>
        /// This demonstrates how to use a callback function which calculates Z values from X and Y
        /// </summary>
        private void SetCallback()
        {
            delRendererFunction f_Callback = delegate (double X, double Y)
            {
                double r = 0.15 * Math.Sqrt(X * X + Y * Y);
                if (r < 1e-10) return 120;
                else return 120 * Math.Sin(r) / r;
            };

            // IMPORTANT: Normalize maintainig the relation between X,Y,Z values otherwise the function will be distorted.
            graph3D.SetFunction(f_Callback, new PointF(-120, -80), new PointF(120, 80), 5, eNormalize.MaintainXYZ);
        }

        /// <summary>
        /// This demonstrates how to use a string formula which calculates Z values from X and Y
        /// </summary>
        private void SetFormula()
        {
            String s_Formula = "12 * sin(x) * cos(y) / (sqrt(sqrt(x * x + y * y)) + 0.2)";
            try
            {
                delRendererFunction f_Function = FunctionCompiler.Compile(s_Formula);

                // IMPORTANT: Normalize maintainig the relation between X,Y,Z values otherwise the function will be distorted.
                graph3D.SetFunction(f_Function, new PointF(-10, -10), new PointF(10, 10), 0.5, eNormalize.MaintainXYZ);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This demonstrates how to set X, Y, Z values directly (without function)
        /// </summary>
        private void SetSurface()
        {
            int[,] s32_Values = new int[,]
            {
                { 34767, 34210, 33718, 33096, 32342, 31851, 31228, 30867, 31457, 30867, 30266, 28934, 27984, 26492, 25167, 25167, 25167},
                { 34669, 34210, 33653, 33096, 32539, 32047, 31490, 30933, 31293, 30671, 29983, 28803, 27886, 26492, 25167, 25167, 25167},
                { 34603, 34144, 33718, 33227, 32768, 32342, 31719, 30999, 31228, 30333, 29622, 28606, 27886, 26492, 25167, 25167, 25167},
                { 34472, 34079, 33653, 33161, 32768, 32408, 31785, 31162, 30802, 30048, 29360, 28312, 27755, 26367, 25049, 25049, 25049},
                { 34210, 33784, 33423, 33161, 32801, 32408, 31785, 31097, 30474, 29622, 29000, 28115, 27623, 26367, 25049, 25049, 25049},
                { 33980, 33587, 33161, 32935, 32588, 32342, 31621, 30802, 29852, 29000, 28377, 27689, 27421, 26367, 25049, 25049, 25049},
                { 33522, 33227, 32702, 32615, 32452, 31851, 30933, 30179, 29295, 28358, 27984, 27132, 27301, 26367, 25049, 25049, 25049},
                { 32672, 32178, 31916, 31469, 31246, 30540, 29852, 29065, 28377, 27623, 27263, 26706, 26935, 26367, 25049, 25049, 25049},
                { 30769, 30423, 29917, 29231, 29392, 28705, 28075, 27726, 27263, 26691, 26417, 26182, 26575, 26575, 25246, 25246, 25246},
                { 27525, 27518, 26701, 27334, 27682, 27402, 26903, 26707, 26444, 25887, 25719, 25690, 26122, 26122, 26122, 26122, 26122},
                { 23475, 23888, 24478, 25330, 26212, 26199, 25701, 25664, 25740, 25013, 24904, 25068, 25374, 25374, 25374, 25374, 25374},
                { 20677, 21445, 22544, 23593, 24441, 24785, 24538, 24644, 24773, 24299, 24062, 24576, 24510, 24510, 24510, 24510, 24510},
                { 18743, 19792, 20808, 22086, 22805, 23167, 23486, 23366, 23757, 23411, 23691, 23822, 23822, 23822, 23822, 23822, 23822},
                { 17334, 18579, 19497, 20775, 21463, 21848, 22288, 22446, 22643, 22446, 22643, 22708, 23069, 23069, 23069, 23069, 23069},
                { 16155, 17236, 18350, 19399, 20251, 20677, 21016, 21332, 21660, 21791, 21889, 21955, 22217, 22217, 22217, 22217, 22217},
                { 14746, 15860, 17039, 17990, 18842, 19595, 20050, 20349, 20546, 20840, 20972, 20972, 21332, 21332, 21332, 21332, 21332},
                { 13337, 14516, 15729, 16679, 17564, 18514, 18907, 19169, 19399, 19661, 19792, 19594, 20152, 20152, 20152, 20152, 20152},
                { 12452, 13435, 14615, 15499, 16253, 17105, 17596, 17924, 18153, 18285, 18428, 18776, 19104, 19104, 19104, 19104, 19104},
                { 11469, 12354, 13533, 14287, 15008, 15925, 16187, 16482, 16690, 16976, 17105, 17302, 17531, 17531, 17531, 17531, 17531},
                { 10486, 11370, 12255, 13009, 13861, 14746, 15172, 15368, 15434, 15630, 15794, 15991, 16351, 16351, 16351, 16351, 16351},
                {  9684, 10387, 11141, 11796, 12546, 13337, 14029, 14320, 14549, 14811, 14939, 15434, 15794, 15794, 15794, 15794, 15794},
                {  9059,  9634, 10617, 11141, 11838, 12681, 13411, 13861, 14121, 14624, 14868, 15172, 15368, 15368, 15368, 15368, 15368},
            };

            cPoint3D[,] i_Points3D = new cPoint3D[s32_Values.GetLength(0), s32_Values.GetLength(1)];
            for (int X = 0; X < s32_Values.GetLength(0); X++)
            {
                for (int Y = 0; Y < s32_Values.GetLength(1); Y++)
                {
                    i_Points3D[X, Y] = new cPoint3D(X * 10, Y * 500, s32_Values[X, Y]);
                }
            }

            // Setting one of the strings = null results in hiding this legend
            graph3D.AxisX_Legend = "MAP (kPa)";
            graph3D.AxisY_Legend = "Engine Speed (rpm)";
            graph3D.AxisZ_Legend = "ADS";

            // IMPORTANT: Normalize X,Y,Z separately because there is an extreme mismatch 
            // between X values (< 300) and Z values (> 30000)
            graph3D.SetSurfacePoints(i_Points3D, eNormalize.Separate);
        }

        /// <summary>
        /// This demonstrates how to set X, Y, Z scatterplot points in form of a spiral.
        /// b_Lines = true --> connect the points with lines.
        /// </summary>
        private void SetScatterPlot(bool b_Lines)
        {
            List<cScatter> i_List = new List<cScatter>();

            for (double P = -22.0; P < 22.0; P += 0.1)
            {
                double X = Math.Sin(P) * P;
                double Y = Math.Cos(P) * P;
                double Z = P;
                if (Z > 0.0) Z /= 3.0;

                i_List.Add(new cScatter(X, Y, Z, null));
            }

            // Depending on your use case you can also specify MaintainXY or MaintainXYZ here
            if (b_Lines)
                graph3D.SetScatterLines(i_List.ToArray(), eNormalize.Separate, 3);
            else
                graph3D.SetScatterPoints(i_List.ToArray(), eNormalize.Separate);
        }

        /// <summary>
        /// This demonstrates how to set X, Y, Z scatterplot points in form of a heart
        /// </summary>
        private void SetValentine()
        {
            List<cScatter> i_List = new List<cScatter>();

            // Upper (round) part of heart
            double X = 0.0;
            double Z = 0.0;
            for (double P = 0.0; P <= Math.PI * 1.32; P += 0.025)
            {
                X = Math.Cos(P) * 1.5 - 1.5;
                Z = Math.Sin(P) * 3.0 + 6.0;
                i_List.Add(new cScatter(X, -X, Z, Brushes.Red));
                i_List.Add(new cScatter(-X, X, Z, Brushes.Red));
            }

            // Lower (linear) part of heart
            double d_X = X / 70;
            double d_Z = Z / 70;
            while (Z >= 0.0)
            {
                i_List.Add(new cScatter(X, -X, Z, Brushes.Red));
                i_List.Add(new cScatter(-X, X, Z, Brushes.Red));
                X -= d_X;
                Z -= d_Z;
            }

            graph3D.SetScatterPoints(i_List.ToArray(), eNormalize.MaintainXYZ);
        }

        private void labelControl20_Click(object sender, EventArgs e)
        {

        }

        private void dockPanel1_Click(object sender, EventArgs e)
        {

        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
    }
    #endregion
}
