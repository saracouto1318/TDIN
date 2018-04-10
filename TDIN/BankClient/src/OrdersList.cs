﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankClient
{
    public partial class OrdersList : Form
    {
        private TableLayoutPanel panel = new TableLayoutPanel();
        private List<Transaction> transactions;

        public OrdersList()
        {
            InitializeComponent();
            ExistTransactions();
            GetUserInforamtionAsync();
        }

        private async void GetUserInforamtionAsync()
        {
            User user = null;
            await Task.Run(() =>
            {
                try
                {
                    user = Services.GetInstance().GetUserInformation();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Program.context.ChangeForm(this, new AuthenticationPage());
                }
            });
            UpdateUserProfile(user);
        }

        private void UpdateUserProfile(User user)
        {
            if (user == null)
            {
                return;
            }
            userName.Text = user.name;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void ExistTransactions()
        {
            if (Services.GetInstance().GetMyTransactions(TransactionType.ALL, false).Count == 0)
            {
                label2.Visible = false;
                comboBox1.Visible = false;
                panel.Visible = false;
                label.Visible = true;
            }
            else
            {
                label.Visible = false;
                transactions = Services.GetInstance().GetMyTransactions(TransactionType.ALL, true);
                CreateTable();
            }
        }

        private TransactionType GetTransactionType(Transaction transaction)
        {
            if(transaction.buyer != null && transaction.buyer == Services.GetInstance().GetUserInformation().username)
            {
                return TransactionType.BUY;
            }
            else
            {
                return TransactionType.SELL;
            }
        }
 
        private void CreateTable()
        {
            if (transactions == null)
            {
                Services.GetInstance().OnExit();
                Program.context.ChangeForm(this, new AuthenticationPage());
                return;
            }

            if(transactions.Count == 0)
            {
                label.Visible = true;
                panel.Visible = false;
                return;
            }

            label.Visible = false;
            panel.Visible = true;

            float value = 100 / (transactions.Count+1);
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize, value));

            CreatePanel();
            panel.Controls.Add(new Label()
            {
                Text = "Diginotes",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            }, 0, 0);
            panel.Controls.Add(new Label()
            {
                Text = "Price",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            }, 1, 0);
            panel.Controls.Add(new Label()
            {
                Text = "Quotation",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            }, 2, 0);
            panel.Controls.Add(new Label()
            {
                Text = "Date",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            }, 3, 0);
            panel.Controls.Add(new Label()
            {
                Text = "Status",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            }, 4, 0);

            int index = 0;
            foreach (Transaction t in transactions)
            {
                int quantity = t.quantity;
                string buyer = t.buyer;
                string seller = t.seller;
                DateTime date = t.date;

                panel.RowStyles.Add(new RowStyle(SizeType.AutoSize, value));

                Label labelTmp = new Label()
                {
                    Text = quantity.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = (buyer == null || seller == null) ? Color.DarkGray: Color.DarkBlue,
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                };

                if (buyer == null || seller == null)
                    labelTmp.Click += (object sender, EventArgs e) =>
                    {
                        Program.context.ChangeForm(this, new EditOrder(t.ID, quantity, GetTransactionType(t)));
                    };

                panel.Controls.Add(labelTmp, 0, index + 1);

                labelTmp = new Label()
                {
                    Text = "2",
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = (buyer == null || seller == null) ? Color.DarkGray : Color.DarkBlue,
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                };

                if (buyer == null || seller == null)
                    labelTmp.Click += (object sender, EventArgs e) =>
                    {
                        Program.context.ChangeForm(this, new EditOrder(t.ID, quantity, GetTransactionType(t)));
                    };

                panel.Controls.Add(labelTmp, 1, index + 1);

                labelTmp = new Label()
                {
                    Text = "3",
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = (buyer == null || seller == null) ? Color.DarkGray : Color.DarkBlue,
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                };

                if (buyer == null || seller == null)
                    labelTmp.Click += (object sender, EventArgs e) =>
                    {
                        Program.context.ChangeForm(this, new EditOrder(t.ID, quantity, GetTransactionType(t)));
                    };

                panel.Controls.Add(labelTmp, 2, index + 1);

                labelTmp = new Label()
                {
                    Text = date.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = (buyer == null || seller == null) ? Color.DarkGray : Color.DarkBlue,
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                };

                if (buyer == null || seller == null)
                    labelTmp.Click += (object sender, EventArgs e) =>
                    {
                        Program.context.ChangeForm(this, new EditOrder(t.ID, quantity, GetTransactionType(t)));
                    };

                panel.Controls.Add(labelTmp, 3, index + 1);

                labelTmp = new Label()
                {
                    Text = (buyer == null || seller == null) ? "Open" : "Closed",
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = (buyer == null || seller == null) ? Color.DarkGray : Color.DarkBlue,
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                };

                if (buyer == null || seller == null)
                    labelTmp.Click += (object sender, EventArgs e) =>
                    {
                        Program.context.ChangeForm(this, new EditOrder(t.ID, quantity, GetTransactionType(t)));
                    };

                panel.Controls.Add(labelTmp, 4, index + 1);

                index++;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "All":
                    transactions = Services.GetInstance().GetMyTransactions(TransactionType.ALL, true);
                    CreateTable();
                    break;
                case "Purchase Orders - Open":
                    transactions = Services.GetInstance().GetMyTransactions(TransactionType.BUY, true);
                    CreateTable();
                    break;
                case "Purchase Orders - Close":
                    transactions = Services.GetInstance().GetMyTransactions(TransactionType.BUY, false);
                    CreateTable();
                    break;
                case "Selling Orders - Open":
                    transactions = Services.GetInstance().GetMyTransactions(TransactionType.SELL, true);
                    CreateTable();
                    break;
                case "Selling Orders - Close":
                    transactions = Services.GetInstance().GetMyTransactions(TransactionType.SELL, false);
                    CreateTable();
                    break;
            }
        }

        private void CreatePanel()
        {
            panel.Dispose();
            panel = new TableLayoutPanel
            {
                BackColor = SystemColors.ButtonHighlight,
                BackgroundImageLayout = ImageLayout.Center,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                ColumnCount = 5
            };

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panel.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, (0));
            panel.Location = new Point(150, 221);
            panel.Name = "tableLayoutPanel1";
            panel.Size = new Size(100, 40);
            panel.AutoSize = true;

            Controls.Add(panel);
        }

        private void OrdersList_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);

            // no larger than screen size
            MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Services.GetInstance().DeleteSession();
            Program.context.ChangeForm(this, new AuthenticationPage());
            Program.exit();
        }
    }
}
