using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmploySet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable T_Emp;
        DataTable T_Pod;
        DataTable T_Dol;
        string path;
        string emp_path;
        string pod_path;
        string dol_path;
        List<Employ> EmployList = new List<Employ>();
        List<Dols> DolList = new List<Dols>();
        List<Pods> PodList = new List<Pods>();
        //{
        //    new Employ("iPhone 6S", "Apple", "222"),
        //    new Employ("Lumia 950", "Microsoft", "333"),
        //    new Employ("Nexus 5X", "Google", "444" )
        //};

        public MainWindow()
        {
            InitializeComponent();

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            path = System.IO.Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            emp_path = System.IO.Path.Combine(path, "EmploySet.xml");
            dol_path = System.IO.Path.Combine(path, "DolSet.xml");
            pod_path = System.IO.Path.Combine(path, "PodSet.xml");
            LoadData();


        }

        private bool LoadData()
        {
            bool res = false;
            
            string[] sss = System.IO.Directory.GetFiles(path, "*.xml");
            if (sss.Length > 0)
            {
                T_Emp = new DataTable("Employ");
                T_Emp.ReadXml(emp_path);
                T_Emp.ReadXmlSchema(emp_path);
                foreach (DataRow RRR in T_Emp.Rows)
                {
                    Employ NewEmp = new Employ(RRR["Fam"].ToString(), RRR["Name"].ToString(), RRR["PatrName"].ToString());
                    EmployList.Add(NewEmp);
                }

                T_Dol = new DataTable("Dol");
                T_Dol.ReadXml(dol_path);
                T_Dol.ReadXmlSchema(dol_path);
                foreach (DataRow RRR in T_Dol.Rows)
                {
                    Dols NewDol = new Dols(RRR["Name"].ToString());
                    DolList.Add(NewDol);
                }

                T_Pod = new DataTable("Pod");
                T_Pod.ReadXml(pod_path);
                T_Pod.ReadXmlSchema(pod_path);
                foreach (DataRow RRR in T_Pod.Rows)
                {
                    Pods NewPod = new Pods(RRR["Name"].ToString());
                    PodList.Add(NewPod);
                }
            }
            else
            {
                MessageBox.Show("Не найдено файлов с данными.", "Программа будет закрыта", MessageBoxButton.OK, MessageBoxImage.Stop);
            }



            return res;
        }

        private void EmpGrid_Loaded(object sender, RoutedEventArgs e)
        {
            EmpGrid.ItemsSource = EmployList;
        }

        private void DolGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DolGrid.ItemsSource = DolList;
        }

        private void PodGrid_Loaded(object sender, RoutedEventArgs e)
        {
            PodGrid.ItemsSource = PodList;
        }

        private void butAdd_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name.Equals("butAdd"))
            {
                EmployList.Add(new Employ(tbFam.Text, tbName.Text, tbPatrName.Text));
                EmpGrid.Items.Refresh();
            }
            if (button.Name.Equals("butAddPod"))
            {
                PodList.Add(new Pods(tbPod.Text));
                PodGrid.Items.Refresh();
            }
            if (button.Name.Equals("butAddDol"))
            {
                DolList.Add(new Dols(tbDol.Text));
                DolGrid.Items.Refresh();
            }
        }

        private void butDel_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name.Equals("butDel"))
            {
                Employ path = EmpGrid.SelectedItem as Employ;
                if (path != null && MessageBox.Show(string.Format("Удалить сотрудника [{0} {1} {2}]?", path.Fam, path.Name, path.PatrName), "Подтвердите операцию", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    EmployList.Remove(path);
                    EmpGrid.Items.Refresh();
                }
            }
            if (button.Name.Equals("butDelPod"))
            {
                Pods path = PodGrid.SelectedItem as Pods;
                if (path != null && MessageBox.Show(string.Format("Удалить подразделение [{0}]?", path.Name), "Подтвердите операцию", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    PodList.Remove(path);
                    PodGrid.Items.Refresh();
                }
            }
            if (button.Name.Equals("butDelDol"))
            {
                Dols path = DolGrid.SelectedItem as Dols;
                if (path != null && MessageBox.Show(string.Format("Удалить должность [{0}]?", path.Name), "Подтвердите операцию", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DolList.Remove(path);
                    DolGrid.Items.Refresh();
                }
            }
        }

        private void tbFam_TextChanged(object sender, TextChangedEventArgs e)
        {
            butAdd.IsEnabled = (tbFam.Text.Length > 0 && tbName.Text.Length > 0 && tbPatrName.Text.Length > 0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            

            T_Emp = new DataTable("Employ");
            T_Emp.Columns.Add(new DataColumn("Fam"));
            T_Emp.Columns.Add(new DataColumn("Name"));
            T_Emp.Columns.Add(new DataColumn("PatrName"));

            foreach (Employ ob in EmployList)
            {
                DataRow RRR = T_Emp.NewRow();
                RRR["Fam"] = ob.Fam;
                RRR["Name"] = ob.Name;
                RRR["PatrName"] = ob.PatrName;
                T_Emp.Rows.Add(RRR);
            }
            T_Emp.AcceptChanges();
            T_Emp.WriteXml(emp_path, XmlWriteMode.WriteSchema);

            T_Dol = new DataTable("Dol");
            T_Dol.Columns.Add(new DataColumn("Name"));
            
            foreach (Dols ob in DolList)
            {
                DataRow RRR = T_Dol.NewRow();
                RRR["Name"] = ob.Name;
                T_Dol.Rows.Add(RRR);
            }
            T_Dol.AcceptChanges();
            T_Dol.WriteXml(dol_path, XmlWriteMode.WriteSchema);

            T_Pod = new DataTable("Pod");
            T_Pod.Columns.Add(new DataColumn("Name"));

            foreach (Pods ob in PodList)
            {
                DataRow RRR = T_Pod.NewRow();
                RRR["Name"] = ob.Name;
                T_Pod.Rows.Add(RRR);
            }
            T_Pod.AcceptChanges();
            T_Pod.WriteXml(pod_path, XmlWriteMode.WriteSchema);
        }

        private void tbFam_MouseEnter(object sender, MouseEventArgs e)
        {
           
        }

        private void tbFam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                if ((bool)textBox.Tag == true)
                {
                    textBox.SelectAll();
                    textBox.Tag = false;
                }

        }

        private void tbFam_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.SelectAll();
        }

        private void tbFam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                textBox.Tag = true;
        }

      

        private void tbPod_TextChanged(object sender, TextChangedEventArgs e)
        {
            butAddPod.IsEnabled = (tbPod.Text.Length > 0);
        }

        private void tbDol_TextChanged(object sender, TextChangedEventArgs e)
        {
            butAddDol.IsEnabled = (tbDol.Text.Length > 0);
        }
    }
}
