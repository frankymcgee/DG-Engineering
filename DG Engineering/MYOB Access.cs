using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace DG_Engineering
{
    public partial class MyobAccess : Form
    {
        public MyobAccess()
        {
            InitializeComponent();
        }

        private async void MyobAccess_Load(object sender, EventArgs e)
        {
            var environment = await CoreWebView2Environment.CreateAsync(null, Path.GetTempPath());
            await MYOBAccessViewer.EnsureCoreWebView2Async(environment);
            MYOBAccessViewer.CoreWebView2.Navigate("https://secure.myob.com/oauth2/account/authorize?client_id=43112a28-1d90-4a9e-97a2-0c5f40f25aef&redirect_uri=https://dgengineering.com.au/authenticated/&response_type=code&scope=CompanyFile");
            MYOBAccessViewer.SourceChanged += MYOBAccessViewer_SourceChanged;
        }

        private void MYOBAccessViewer_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            var url = MYOBAccessViewer.Source.ToString();
            if (!url.Contains("authenticated")) return;
            Static.UrlCoded = url.Split('=')[1];
        }
    }
}
