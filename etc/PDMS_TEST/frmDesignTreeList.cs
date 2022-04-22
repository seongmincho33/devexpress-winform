using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Forms;
using Aveva.Pdms.Database;
using Aveva.Pdms.Geometry;
using PDMS_TEST.Properties;
using NOUN = Aveva.Pdms.Database.DbElementTypeInstance;

namespace PDMS_TEST
{
    public partial class frmDesignTreeList : Form
    {
        private DataTable frmDataTable { get; set; }
        private ElementType elementType { get; set; }
        private DbElement SelectedDbElement = null;
        private enum ElementType
        {
            SITE,
            ZONE,
            STRUCUTRE,
            EQUIPMENT,
            BOX,
            POGON,
            POLYHEDRON,
            POHEDRON,
            EXTRUSION,
            LOOP,
            VERTEX
        }
        
        public frmDesignTreeList(DataTable dataTable)
        {
            this.frmDataTable = new DataTable();
            this.frmDataTable = dataTable;            
            InitializeComponent();      
            this.Load += FrmDesignTreeList_Load;
            this.SetControls();
        }

        #region 컨트롤 셋팅
        private void SetControls()
        {
            this.SetRadioButtons();
            this.SetSimpleButtons();
            this.treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
            void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
            {
                try
                {
                    this.SelectedDbElement = (DbElement)(e.Node.Tag);
                    this.SetMessageToRichTextBox("선택한 값 불러오기 성공\n" + e.Node.Text);
                }
                catch (Exception ex)
                {
                    this.SetMessageToRichTextBox(ex);
                }
            }
        }        
        private void SetSimpleButtons()
        {
            this.btnCreateElement.Click += BtnCreateElement_Click;
            this.btnElementType.Click += BtnElementType_Click;
            this.btnAttribute.Click += BtnAttribute_Click;
            this.btnDifference.Click += BtnDifference_Click;
            this.btnTest.Click += BtnTest_Click;
            this.btnEditElement.Click += BtnEditElement_Click;
            this.btnCreatePolygon.Click += BtnCreatePolygon_Click;
        }   

        private void SetRadioButtons()
        {
            this.rdSITE.Click += (sender, e) => { this.elementType = ElementType.SITE; };
            this.rdZONE.Click += (sender, e) => { this.elementType = ElementType.ZONE; };
            this.rdSTRUCTURE.Click += (sender, e) => { this.elementType = ElementType.STRUCUTRE; };
            this.rdEQUPMENT.Click += (sender, e) => { this.elementType = ElementType.EQUIPMENT; };
            this.rdBOX.Click += (sender, e) => { this.elementType = ElementType.BOX; };
            this.rdPOLYHEDRON.Click += (sender, e) => { this.elementType = ElementType.POLYHEDRON; };
            this.rdPOGON.Click += (sender, e) => { this.elementType = ElementType.POGON; };
            this.rdPOHEDRON.Click += (sender, e) => { this.elementType = ElementType.POHEDRON; };
            this.rdEXTRUSION.Click += (sender, e) => { this.elementType = ElementType.EXTRUSION; };
            this.rdLOOP.Click += (sender, e) => { this.elementType = ElementType.LOOP; };
            this.rdVERTEX.Click += (sender, e) => { this.elementType = ElementType.VERTEX; };            
        }   
       
        #endregion

        #region 이벤트
        private void BtnTest_Click(object sender, EventArgs e)
        {
            this.Test();
        }
        private void BtnDifference_Click(object sender, EventArgs e)
        {
            this.DifferenceBetweenPropertiesAndAttributes(this.SelectedDbElement);
        }
        private void BtnAttribute_Click(object sender, EventArgs e)
        {
            this.ShowAttribute(this.SelectedDbElement);
        }
        private void BtnElementType_Click(object sender, EventArgs e)
        {
            this.ShowElementType(this.SelectedDbElement);
        }
        private void BtnEditElement_Click(object sender, EventArgs e)
        {
            //엘리먼트 수정버튼
            try
            {
                if(elementType == ElementType.VERTEX)
                {                    
                    this.SelectedDbElement.SetAttribute(DbAttributeInstance.POS, this.GetGeometryPosition());
                    this.SetMessageToRichTextBox(this.SelectedDbElement.ToString() + "엘리먼트 수정 성공!");
                }
                else if(elementType == ElementType.EXTRUSION)
                {
                    this.SelectedDbElement.SetAttribute(DbAttributeInstance.HEIG, this.StringToDouble(this.txtExtrusionHeight.Text));
                    this.SetMessageToRichTextBox(this.SelectedDbElement.ToString() + "엘리먼트 수정 성공!");
                }                
                else
                {
                    this.SetMessageToRichTextBox(this.SelectedDbElement.ToString() + "수정하려는 element type을 선택해주세요.");
                }
            }
            catch(Exception ex)
            {
                this.SetMessageToRichTextBox(ex);
            }                       
        }
        private void BtnCreatePolygon_Click(object sender, EventArgs e)
        {           
            try
            {
                DbElement tempElement = DbElement.GetElement("/*");
                tempElement = this.CreateDbElement(NOUN.SITE, tempElement, "PEDAS_Site");
                tempElement = this.CreateDbElement(NOUN.ZONE, tempElement, "PEDAS_Zone");
                tempElement = this.CreateDbElement(NOUN.EQUIPMENT, tempElement, "PEDAS_Equipment");
                tempElement = this.CreateDbElement(NOUN.EXTRUSION, tempElement, "PEDAS_Extrusion");
                tempElement.SetAttribute(DbAttributeInstance.HEIG, this.StringToDouble(this.txtExtrusionHeightPEDAS.Text));
                tempElement = this.CreateDbElement(NOUN.LOOP, tempElement, "PEDAS_Loop");
                string[] points = this.richTxtBoxPointInput.Text
                    .Remove(this.richTxtBoxPointInput.Text.Length -1)
                    .Substring(7)
                    .Split("\n".ToCharArray());
                int i = 0;
                foreach (var point in points)
                {
                    string[] point_splited = point.Split("\t".ToCharArray());
                    this.CreateDbElement(NOUN.VERTEX, tempElement, "PEDAS_Vertex" + i++.ToString())
                        .SetAttribute(DbAttributeInstance.POS, this.GetGeometryPosition(
                        this.StringToDouble(point_splited[0]) * 1000
                        , this.StringToDouble(point_splited[1]) * 1000
                        , this.StringToDouble(point_splited[2]) * 1000
                        ));
                    this.SetMessageToRichTextBox("Vertex 생성 성공!", true);
                }
            }
            catch(Exception ex)
            {
                this.SetMessageToRichTextBox(ex);
            }
           
        }
        private void FrmDesignTreeList_Load(object sender, EventArgs e)
        {
            this.SetTreeView();
            // Set window location
            if (Settings.Default.WindowLocation != null)
            {
                this.Location = Settings.Default.WindowLocation;
            }

            // Set window size
            if (Settings.Default.WindowSize != null)
            {
                this.Size = Settings.Default.WindowSize;
            }
            this.FormClosing += FrmDesignTreeList_FormClosing;
        }
        private void FrmDesignTreeList_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Copy window location to app settings
            Settings.Default.WindowLocation = this.Location;

            // Copy window size to app settings
            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Default.WindowSize = this.Size;
            }
            else
            {
                Settings.Default.WindowSize = this.RestoreBounds.Size;
            }

            // Save settings
            Settings.Default.Save();
        }
        private void BtnCreateElement_Click(object sender, EventArgs e)
        {
            try
            {
                if (elementType == ElementType.SITE)
                {                 
                    CreateDbElement(NOUN.SITE);
                }
                else if (elementType == ElementType.ZONE)
                {                   
                    CreateDbElement(NOUN.ZONE);
                }
                else if (elementType == ElementType.STRUCUTRE)
                {                  
                    CreateDbElement(NOUN.STRUCTURE);
                }
                else if (elementType == ElementType.EQUIPMENT)
                {                    
                    CreateDbElement(NOUN.EQUIPMENT);
                }
                else if (elementType == ElementType.BOX)
                {                  
                    CreateDbElement(NOUN.BOX);
                }
                else if (elementType == ElementType.POLYHEDRON)
                {                  
                    CreateDbElement(NOUN.POLYHEDRON);
                }
                else if (elementType == ElementType.POGON)
                {                   
                    CreateDbElement(NOUN.POGON);
                }
                else if (elementType == ElementType.POHEDRON)
                {                    
                    CreateDbElement(NOUN.POHEDRON);
                }
                else if (elementType == ElementType.EXTRUSION)
                {
                    CreateDbElement(NOUN.EXTRUSION);                    
                }
                else if (elementType == ElementType.LOOP)
                {
                    CreateDbElement(NOUN.LOOP);
                }
                else if (elementType == ElementType.VERTEX)
                {                                       
                    CreateDbElement(NOUN.VERTEX).SetAttribute(DbAttributeInstance.POS, this.GetGeometryPosition());
                }
            }
            catch (Exception ex)
            {
                this.SetMessageToRichTextBox(ex);
            }                 
        }
        private DbElement CreateDbElement(DbElementType dbElementType, DbElement OwnerElement = null, string elementName = "TEST")
        {
            DbElement dbElement = null;
            if (OwnerElement == null)
            {
                dbElement = this.SelectedDbElement.Create(1, dbElementType);
            }
            else
            {
                dbElement = OwnerElement.Create(1, dbElementType);
            }
            
            if(elementName == "TEST")
            {
                dbElement.SetAttribute(DbAttributeInstance.NAME, "/" + this.txtDbElemnetName.Text);
                this.SetMessageToRichTextBox(this.txtDbElemnetName.Text + " 엘리먼트 생성 성공");
            }
            else
            {
                dbElement.SetAttribute(DbAttributeInstance.NAME, "/" + elementName);
                this.SetMessageToRichTextBox(this.txtDbElemnetName.Text + " 엘리먼트 생성 성공");
            }            
            return dbElement;
        }
        private Position GetGeometryPosition()
        {
            Aveva.Pdms.Geometry.Position point = Aveva.Pdms.Geometry.Position.Create();
            point.X = StringToDouble(this.txtVertexPosX.Text);
            point.Y = StringToDouble(this.txtVertexPosY.Text);
            point.Z = StringToDouble(this.txtVertexPosZ.Text);
            return point;
        }
        private Position GetGeometryPosition(double x, double y, double z)
        {
            Aveva.Pdms.Geometry.Position point = Aveva.Pdms.Geometry.Position.Create();
            point.X = x;
            point.Y = y;
            point.Z = z;
            return point;
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
                else
                {
                    this.SetMessageToRichTextBox("문자 -> double 형변환 실패", true);
                }
            }
            catch(Exception ex) 
            {
                this.SetMessageToRichTextBox(ex);
            }           
            return d;
        }
        private void SetTreeView()
        {
            //Use a DataSet to manage the data
            try
            {
                DataSet ds = new DataSet();

                ds.Tables.Add(this.frmDataTable);

                //add a relationship

                ds.Relations.Add("rsParentChild", ds.Tables["data"].Columns["NAME"],

                    ds.Tables["data"].Columns["OWNER"], false);

                foreach (DataRow dr in ds.Tables["data"].Rows)
                {
                    if (dr["OWNER"] == DBNull.Value)
                    {
                        TreeNode root = new TreeNode(dr["NAME"].ToString());
                        root.Tag = dr["DBELEMENT"];
                        treeView1.Nodes.Add(root);
                        PopulateTree(dr, root);
                    }
                }
                //treeView1.ExpandAll();                

                //treeList1.ParentFieldName = "OWNER";
                //treeList1.KeyFieldName = "NAME";
                //treeList1.DataSource = this.frmDataTable;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                this.SetMessageToRichTextBox(ex);
            }
        }
        public void PopulateTree(DataRow dr, TreeNode pNode)
        {
            foreach (DataRow row in dr.GetChildRows("rsParentChild"))
            {
                TreeNode cChild = new TreeNode(row["NAME"].ToString());
                cChild.Tag = row["DBELEMENT"];
                pNode.Nodes.Add(cChild);
                //Recursively build the tree
                PopulateTree(row, cChild);
            }
        }
        #endregion

        #region 사용메서드

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
                    this.SetMessageToRichTextBox(message.ToString());
                }
            }
            catch (Exception ex)
            {
                this.SetMessageToRichTextBox(ex);
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
                    
                    this.SetMessageToRichTextBox(message.ToString());
                }
            }
            catch (Exception ex)
            {
                this.SetMessageToRichTextBox(ex);
            }
        }

        /// <summary>
        /// 선택한 DbElement의 element.GetElementType()를 확인합니다. 
        /// </summary>
        private void ShowElementType(DbElement element)
        {
            try
            {
                //DbElement element = Aveva.Pdms.Shared.CurrentElement.Element;
                if (MessageBox.Show("클릭한 DbElement의 타입을 보시겠습니까?", "element.GetElementType()", MessageBoxButtons.OKCancel) != DialogResult.Cancel)
                {                   
                    this.SetMessageToRichTextBox("!!현재 클릭한 DbElement의 타입!! = "
                        + element.GetElementType()
                        + "\n\n"
                        + "아래는 현재 클릭한 DbElement의 프로퍼티 값들 입니다.(GetAttributes()아님주의)"
                        + this.GetPropertyString(element));
                }
            }
            catch(Exception ex)
            {
                this.SetMessageToRichTextBox(ex);
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
            try
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(component))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(component);
                    object propertyType = descriptor.PropertyType;
                    message.AppendLine(String.Format("({0}) {1} = {2}", propertyType, name, value));
                }                
            }
            catch(Exception ex)
            {
                this.SetMessageToRichTextBox(ex);
            }
            return message.ToString();
        }

        private void SetMessageToRichTextBox(Exception ex, bool append = false)
        {   
            if (append)
            {
                this.richTxtBoxResult.AppendText(ex.ToString());
            }
            else
            {
                this.richTxtBoxResult.Text = ex.ToString();
            }
        }

        private void SetMessageToRichTextBox(string message, bool append = false)
        {
            if (append)
            {
                this.richTxtBoxResult.AppendText(message);
            }
            else
            {
                this.richTxtBoxResult.Text = message;
            }                    
        }

        #endregion

        #region 테스트 메서드
        private void Test()
        {
            try
            {
                this.SetMessageToRichTextBox("아래는 EXTRUSION이랑 관련된것같은 모든 DbElementTypeInstance불러옴 \n\n[NOUN.EXTRUSION]\n", false);
                this.SetMessageToRichTextBox(this.GetPropertyString(NOUN.EXTRUSION), true);
                this.SetMessageToRichTextBox("\n[NOUN.AEXTRUSION]\n", true);
                this.SetMessageToRichTextBox(this.GetPropertyString(NOUN.AEXTRUSION), true);
                this.SetMessageToRichTextBox("\n[NOUN.NSEXTRUSION]\n", true);
                this.SetMessageToRichTextBox(this.GetPropertyString(NOUN.NSEXTRUSION), true);
                this.SetMessageToRichTextBox("\n[NOUN.SEXTRUSION]\n", true);
                this.SetMessageToRichTextBox(this.GetPropertyString(NOUN.SEXTRUSION), true);
                                
            }
            catch(Exception ex)
            {
                this.SetMessageToRichTextBox(ex);
            }
        }

        private void GetRTFtableFromRichTextBox()
        {            
            this.FindTableinRtf(this.richTxtBoxPointInput.Rtf);
        }
        private void FindTableinRtf(string rtf)
        {
            var flowDocument = new FlowDocument();
            var textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(rtf)))
            {
                textRange.Load(ms, DataFormats.Rtf);
            }
            var blocks = flowDocument.Blocks;
            foreach (var block in flowDocument.Blocks)
            {
                switch (block)
                {
                    case List list:
                        //implement List;
                        break;

                    case Table table:
                        WorkWithTable(table);
                        break;

                    case Paragraph paragraph:
                        
                        break;
                    case Section section:
                        
                        break;

                }
            }

        }
        private void WorkWithTable(Table rtfTable)
        {            
            TableColumnCollection columns = rtfTable.Columns;
            TableRowGroupCollection rowGroups = rtfTable.RowGroups;            
            foreach (var row in rowGroups[0].Rows)
            {                               
                _ = row.Cells[0];
                _ = row.Cells[1];
                _ = row.Cells[2];
                //this.GetGeometryPosition((double)row.Cells[0].GetValue(), (double)row.Cells[1], (double)row.Cells[2]);
            }
            
        }
        #endregion

        #region 참고용

        // Populates a TreeView control with example nodes. 
        private void InitializeTreeView()
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Add("Parent");
            treeView1.Nodes[0].Nodes.Add("Child 1");
            treeView1.Nodes[0].Nodes.Add("Child 2");
            treeView1.Nodes[0].Nodes[1].Nodes.Add("Grandchild");
            treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Great Grandchild");
            treeView1.EndUpdate();        
        }

        // Create a new ArrayList to hold the Customer objects.
        private ArrayList customerArray = new ArrayList();

        private void FillMyTreeView()
        {
            // Add customers to the ArrayList of Customer objects.
            for (int x = 0; x < 1000; x++)
            {
                customerArray.Add(new Customer("Customer" + x.ToString()));
            }

            // Add orders to each Customer object in the ArrayList.
            foreach (Customer customer1 in customerArray)
            {
                for (int y = 0; y < 15; y++)
                {
                    customer1.CustomerOrders.Add(new Order("Order" + y.ToString()));
                }
            }           

            // Suppress repainting the TreeView until all the objects have been created.
            treeView1.BeginUpdate();

            // Clear the TreeView each time the method is called.
            treeView1.Nodes.Clear();

            // Add a root TreeNode for each Customer object in the ArrayList.
            foreach (Customer customer2 in customerArray)
            {
                treeView1.Nodes.Add(new TreeNode(customer2.CustomerName));

                // Add a child treenode for each Order object in the current Customer object.
                foreach (Order order1 in customer2.CustomerOrders)
                {
                    treeView1.Nodes[customerArray.IndexOf(customer2)].Nodes.Add(
                      new TreeNode(customer2.CustomerName + "." + order1.OrderID));
                }
            }            

            // Begin repainting the TreeView.
            treeView1.EndUpdate();
        }

        //private void SetTreeListControl()
        //{
        //    this.treeList1.KeyFieldName = "NAME";
        //    this.treeList1.ParentFieldName = "OWNER";
        //    this.treeList1.RootValue = 0;
        //    this.treeList1.DataSource = frmDataTable;
        //}
        //private void DataRetrieve()
        //{
        //    try
        //    {
        //        this.treeList1.DataBindings.Clear();
        //        this.treeList1.DataSource = null;
        //        this.treeList1.DataSource = frmDataTable;
        //        this.treeList1.PopulateColumns();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}






        //private void SetTreeView()
        //{
        //    try
        //    {
        //        var source = this.frmDataTable.AsEnumerable();
        //        var nodes = GetTreeNodes(
        //                source,
        //                (r) => r.Field<string>("OWNER") == null,
        //                (r, s) => s.Where(x => r["NAME"].Equals(x["OWNER"])),
        //                (r) => new TreeNode { Text = r.Field<DbElement>("DBELEMENT").ToString() }
        //        );
        //        this.treeView1.Nodes.AddRange(nodes.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        //private IEnumerable<TreeNode> GetTreeNodes<T>(
        //    IEnumerable<T> source,
        //    Func<T, Boolean> isRoot,
        //    Func<T, IEnumerable<T>, IEnumerable<T>> getChilds,
        //    Func<T, TreeNode> getItem)
        //{
        //    IEnumerable<T> roots = source.Where(x => isRoot(x));
        //    foreach (T root in roots)
        //        yield return ConvertEntityToTreeNode(root, source, getChilds, getItem); ;
        //}

        //private TreeNode ConvertEntityToTreeNode<T>(
        //    T entity,
        //    IEnumerable<T> source,
        //    Func<T, IEnumerable<T>, IEnumerable<T>> getChilds,
        //    Func<T, TreeNode> getItem)
        //{
        //    TreeNode node = getItem(entity);
        //    var childs = getChilds(entity, source);
        //    foreach (T child in childs)
        //        node.Nodes.Add(ConvertEntityToTreeNode(child, source, getChilds, getItem));
        //    return node;
        //}
        #endregion
    }
}
