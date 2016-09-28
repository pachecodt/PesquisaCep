using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace PesquisarCepOnline
{
    public partial class frmPrincipal : Form
    {
        private BackgroundWorker bw = new BackgroundWorker();
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        DataSet ds = new DataSet();

        public frmPrincipal()
        {
            InitializeComponent();

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }


        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!VerificarAcessoWeb.EstaConectado())
            {
                MessageBox.Show("Sem Acesso a Web.\nVerifique sua conexão!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                
                bw.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string xml = "http://cep.republicavirtual.com.br/web_cep.php?cep=@cep&formato=xml".Replace("@cep", txtCep.Text);
            ds.ReadXml(xml);
         //   _resetEvent.Set();
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            if (!(e.Error == null))
            {
                this.label6.Text = ("Error: " + e.Error.Message);
            }

            else
            {
                this.label6.Text = "Encontrado!";
            }

            txtLogradouro.Text = ds.Tables[0].Rows[0]["logradouro"].ToString();
            txtBairro.Text = ds.Tables[0].Rows[0]["bairro"].ToString();
            txtCidade.Text = ds.Tables[0].Rows[0]["cidade"].ToString();
            txtUf.Text = ds.Tables[0].Rows[0]["uf"].ToString();
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.label6.Text = (e.ProgressPercentage.ToString() + "%");
        }
    }
}

