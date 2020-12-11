﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PA6
{
    public partial class frmMain : Form
    {
        string cwid;
        List<Book> myBooks;
        public frmMain(string tempCwid)
        {
            this.cwid = tempCwid;
            InitializeComponent();
            pbCover.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadList();
        }
        private void LoadList()
        {
            myBooks = BookFile.GetAllBooks(cwid);
            Books.DataSource = myBooks;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Books_SelectedIndexChanged(object sender, EventArgs e)
        {
            Book myBook = (Book)Books.SelectedItem;
            txtTitleData.Text = myBook.title;
            txtAuthorData.Text = myBook.author;
            txtGenreData.Text = myBook.genre;
            txtCopiesAvailableData.Text = myBook.copies.ToString();
            txtIsbnData.Text = myBook.isbn;
            txtLengthData.Text = myBook.length.ToString();

            try
            {
                pbCover.Load(myBook.cover);
            }
            catch
            {

            }
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            Book myBook = (Book)Books.SelectedItem;
            myBook.copies--;
            BookFile.SaveBook(myBook, cwid, "edit");
            LoadList();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Book myBook = (Book)Books.SelectedItem;
            myBook.copies++;
            BookFile.SaveBook(myBook, cwid, "edit");
            LoadList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Book myBook = (Book)Books.SelectedItem;

            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to delete?", "Delete", MessageBoxButtons.YesNo);

            if(dialogResult == DialogResult.Yes)
            {
                BookFile.DeleteBook(myBook, cwid);
                LoadList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Book myBook = (Book)Books.SelectedItem;

            frmEdit2 myForm = new frmEdit2(myBook,"edit",cwid);
            if(myForm.ShowDialog() == DialogResult.OK)
            {

            }
            else
            {
                LoadList();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Book myBook = new Book();

            frmEdit2 myForm = new frmEdit2(myBook, "new", cwid);
            if (myForm.ShowDialog() == DialogResult.OK)
            {

            }
            else
            {
                LoadList();
            }
        }
    }
}
