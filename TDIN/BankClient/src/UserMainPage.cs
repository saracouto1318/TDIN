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
    public partial class UserMainPage : Form
    {
        public UserMainPage()
        {
            InitializeComponent();
            GetUserInformationAsync();
        }

        private async void GetUserInformationAsync()
        {
            User user = null;
            float power = 1f;
            await Task.Run(() =>
            {
                try
                {
                    user = Services.GetInstance().GetUserInformation();
                    power = Services.GetInstance().GetPower();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Program.context.ChangeForm(this, new AuthenticationPage());
                }
            });
            UpdateUserInformation(user, power);
        }

        private void UpdateUserInformation(User user, float power)
        {
            if(user == null)
            {
                return;
            }
            userName.Text = user.name;
            nDiginotes.Text = user.availableDiginotes + "/" + user.totalDiginotes;
            quotation.Text = power + "$";
            balance.Text = user.balance + "$";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new EditProfile());
        }

        private void addFunds_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new AddFunds());
        }
    }
}
