using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;
using Accounting.DataLayer.Context;

namespace Accounting.App
{
    public partial class frmLogin : Form
    {
        public bool Edited = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (Edited)
            {
                this.Text = "Seting";
                btnlogin.Text = "Save change";
                using (UnitOfWork db = new UnitOfWork())
                {
                    var login = db.LoginRepository.Get().First();
                    txtUserName.Text = login.UserName;
                    txtPassword.Text = login.Password;
                }
            }
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    if (Edited)
                    {
                        
                        var login = db.LoginRepository.Get().First();
                        login.UserName = txtUserName.Text;
                        db.LoginRepository.Update(login);
                        login.Password = txtPassword.Text;

                        
                        db.Save();
                        Application.Restart();
                    }
                    else
                    {
                        if (db.LoginRepository.Get(l => l.UserName == txtUserName.Text && l.Password == txtPassword.Text).Any())
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            RtlMessageBox.Show("Invalid User");
                        }
                    }
                }
            }
        }
    }
}
