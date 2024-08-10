using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitap7
{
    public partial class StudentForm : Form
    {
        private SchoolContext context;
        private int currentIndex = -1;

        public StudentForm()
        {
            InitializeComponent();
            context = new SchoolContext();
            LoadData();
            LoadMajors();
        }

        private void LoadData()
        {
            dataGridViewStudents.DataSource = context.Students.ToList();
        }

        private void LoadMajors()
        {
            comboBoxMajor.Items.AddRange(new string[] { "Công nghệ thông tin", "Quản trị kinh doanh", "Tài chính", "Kế toán", "Kỹ thuật điện", "Cơ khí", "Xây dựng", "Ngôn ngữ Anh" });
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var student = new Student
            {
                FullName = textBoxFullName.Text,
                Age = int.Parse(textBoxAge.Text),
                Major = comboBoxMajor.SelectedItem.ToString()
            };

            context.Students.Add(student);
            context.SaveChanges();
            LoadData();
            ClearFields();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count > 0)
            {
                int studentId = (int)dataGridViewStudents.SelectedRows[0].Cells[0].Value;
                var student = context.Students.Find(studentId);
                context.Students.Remove(student);
                context.SaveChanges();
                LoadData();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count > 0)
            {
                int studentId = (int)dataGridViewStudents.SelectedRows[0].Cells[0].Value;
                var student = context.Students.Find(studentId);
                student.FullName = textBoxFullName.Text;
                student.Age = int.Parse(textBoxAge.Text);
                student.Major = comboBoxMajor.SelectedItem.ToString();

                context.SaveChanges();
                LoadData();
                ClearFields();
            }
        }

        private void ClearFields()
        {
            textBoxFullName.Clear();
            textBoxAge.Clear();
            comboBoxMajor.SelectedIndex = -1;
        }

        private void DisplayStudent(int index)
        {
            var student = context.Students.ToList()[index];
            textBoxFullName.Text = student.FullName;
            textBoxAge.Text = student.Age.ToString();
            comboBoxMajor.SelectedItem = student.Major;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < context.Students.Count() - 1)
            {
                currentIndex++;
                DisplayStudent(currentIndex);
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayStudent(currentIndex);
            }
        }

        private void dataGridViewStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count > 0)
            {
                currentIndex = dataGridViewStudents.SelectedRows[0].Index;
                DisplayStudent(currentIndex);
            }
        }
    }
}
