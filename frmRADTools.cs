// =============================================================================== 
// 		Form Name: frmRADTools.cs
// 		Object purpose: The class is designed for manipulating data in a database table. 
// 		It can be used to fetch data via data access component and return as a DataReader 
// 		or DataSet. This component could be fit in the position of middle tier if the system  
// 		is designed as multi-tiers. 
// 
// 		Designed by: Ben Li. 
// 		Written by:  Ben Li. 
//
// 		Codes generated Date: 05/08/2005 2:50:38 PM.
// 		Last Modified Date:   22/01/2014 12:22:00 PM.
// 
// =============================================================================== 
// Copyright (C) 2005 Mizuho Corporate Bank, Limited Sydney Branch.
// All rights reserved. 
// 
// THIS CODES ARE GENERATED AUTOMATICLLY AND INFORMATION IS PROVIDED 'AS IS' 
// WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT 
// NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS 
// FOR A PARTICULAR PURPOSE.
// =============================================================================== 

using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using mhcb.cs.Shared;

namespace mhcb.EntityHelper
{
	public partial class frmRADTools : Form
	{
		[STAThread]
		static void Main()
		{
			Application.Run(new frmRADTools());
		}

		public frmRADTools()
		{
			InitializeComponent();

			// construct connection string by class field of ConnectionString
			var serverSettings = ServerSettingsHelper.getSettings();

			textBoxServer.Text = serverSettings.SqlServer;
			textBoxDbName.Text = serverSettings.Database;

			if (serverSettings.IntegratedSecurity)
			{
				chkIntegratedSecurity.Checked = true;
				textBoxUserName.Text = serverSettings.SqlUser;
				textBoxPassword.Text = "************";
			}
			else
			{
				chkIntegratedSecurity.Checked = false;
				textBoxUserName.Text = serverSettings.SqlUser;
				textBoxPassword.Text = serverSettings.SqlPass;
			}

			textBoxServer.Enabled = false;
			textBoxDbName.Enabled = false;
			textBoxUserName.Enabled = false;
			textBoxPassword.Enabled = false;
			chkIntegratedSecurity.Enabled = false;

			getSchemaTableCheckListBox();
		}


		private void getSchemaTableCheckListBox()
		{
			var oclsData = new DataSvc(textBoxServer.Text, textBoxDbName.Text, textBoxUserName.Text, textBoxPassword.Text);

			var dr = oclsData.getSchemaTables_dr();

			chklstSchemaTables.Items.Clear();
			while (dr.Read())
			{
				chklstSchemaTables.Items.Add(dr.GetString(0));
			}

			chklstSchemaTables.CheckOnClick = true;
		}

		private void getSchemaViewCheckListBox()
		{
			var oclsData = new DataSvc(textBoxServer.Text, textBoxDbName.Text, textBoxUserName.Text, textBoxPassword.Text);

			var dr = oclsData.getSchemaViews_dr();

			chklstSchemaTables.Items.Clear();
			while (dr.Read())
			{
				chklstSchemaTables.Items.Add(dr.GetString(0));
			}

			chklstSchemaTables.CheckOnClick = true;
		}


		private void getSPCheckListBox()
		{
			var oclsData = new DataSvc(textBoxServer.Text, textBoxDbName.Text, textBoxUserName.Text, textBoxPassword.Text);
			var dr = oclsData.drStoredProcs();

			chklstSchemaTables.Items.Clear();
			while (dr.Read())
			{
				chklstSchemaTables.Items.Add(dr.GetString(0));
			}

			chklstSchemaTables.CheckOnClick = true;
		}



		private void CreateEntity(object sender, EventArgs e)
		{
			string cMsg = "Process was fail!";
			cMsg = "No table is selected.";

			if (chklstSchemaTables.CheckedItems.Count > 0)
			{
				if (rdoBtnCS.Checked)
				{
					var oclsTableCs = new EntityCs();
					cMsg = oclsTableCs.EntityClassCs(chklstSchemaTables, rdoWCF.Checked, rdoEDM.Checked, rdoOther.Checked);

					txtNotePad.Text = "";
					txtNotePad.Text = oclsTableCs.Output;
				}
				else
				{
					var oclsTableVb = new EntityVb();
					cMsg = oclsTableVb.EntityClassVb(chklstSchemaTables, rdoWCF.Checked, rdoEDM.Checked, rdoOther.Checked);

					txtNotePad.Text = "";
					txtNotePad.Text = oclsTableVb.output;
				}

				Clipboard.Clear();						//Clear if any old value is there in Clipboard        
				Clipboard.SetText(txtNotePad.Text);     //Copy text to Clipboard
			}
			
			MessageBox.Show(cMsg, "Entity helper", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}



		private void CreateEntityByView(object sender, EventArgs e)
		{
			string cMsg = "Process was fail!";
			cMsg = "No table is selected.";

			if (chklstSchemaTables.CheckedItems.Count > 0)
			{
				if (rdoBtnCS.Checked)
				{
					var oclsTableCs = new EntityCs();
					cMsg = oclsTableCs.EntityClassCs(chklstSchemaTables, rdoWCF3.Checked, rdoEDM3.Checked, rdoOther3.Checked);

					txtNotePad.Text = "";
					txtNotePad.Text = oclsTableCs.Output;
				}
				else
				{
					var oclsTableVb = new EntityVb();
					cMsg = oclsTableVb.EntityClassVb(chklstSchemaTables, rdoWCF3.Checked, rdoEDM3.Checked, rdoOther3.Checked);

					txtNotePad.Text = "";
					txtNotePad.Text = oclsTableVb.output;
				}

				Clipboard.Clear();						//Clear if any old value is there in Clipboard        
				Clipboard.SetText(txtNotePad.Text);     //Copy text to Clipboard
			}


			MessageBox.Show(cMsg, "Entity helper", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}


		private void CreateSPModel(object sender, EventArgs e)
		{

			var oclsSPGen = new SPGen(textBoxServer.Text, textBoxDbName.Text, textBoxUserName.Text, textBoxPassword.Text);

			txtNotePad.Text = "";
			string cMsg = "Process was fail!";
			cMsg = "No SP option is selected.";

			if (chkSPUpdate.Checked)
			{
				if (chklstSchemaTables.CheckedItems.Count > 0)		//if (dr != null)  
				{
					cMsg = oclsSPGen.CreateSPModelFilesV2(chklstSchemaTables, SPGen.StoredProcedureTypes.UPDATE);
					cMsg = "The Update " + cMsg;

					txtNotePad.Text = oclsSPGen.output;
					Clipboard.Clear();						//Clear if any old value is there in Clipboard        
					Clipboard.SetText(txtNotePad.Text);		//Copy text to Clipboard
				}
				else
				{
					cMsg = "No table is selected.";
				}
			}


			if (chkSPInsert.Checked)
			{
				if (chklstSchemaTables.CheckedItems.Count > 0)		//if (dr != null)
				{
					cMsg = oclsSPGen.CreateSPModelFilesV2(chklstSchemaTables, SPGen.StoredProcedureTypes.INSERT);
					cMsg = "The Insert " + cMsg;

					txtNotePad.Text = oclsSPGen.output;
					Clipboard.Clear();						//Clear if any old value is there in Clipboard        
					Clipboard.SetText(txtNotePad.Text); //Copy text to Clipboard
				}
				else
				{
					cMsg = "No table is selected.";
				}
			}

			 MessageBox.Show(cMsg, "Stored procedure helper", MessageBoxButtons.OK, MessageBoxIcon.Information);

		}

		private void CreateBusinessModel(object sender, EventArgs e)
		{
			string cMsg = "Process was fail!";
			cMsg = "No stored procedure is selected.";

			if (chklstSchemaTables.CheckedItems.Count > 0)		//if (dr != null)  //(dr.HasRows)
			{
				if (rdoBtnCS2.Checked)
				{
					var oclsBusinessServiceCs = new BusinessSvcCs(textBoxServer.Text, textBoxDbName.Text, textBoxUserName.Text, textBoxPassword.Text);
					cMsg = oclsBusinessServiceCs.CreateBsClassFilesCsV2(chklstSchemaTables);

					txtNotePad.Text = "";
					txtNotePad.Text = oclsBusinessServiceCs.output;

					Clipboard.Clear();						//Clear if any old value is there in Clipboard        
					Clipboard.SetText(txtNotePad.Text);		//Copy text to Clipboard
				} 
				else 
				{
					var oclsBusinessServiceVb = new BusinessSvcVb(textBoxServer.Text, textBoxDbName.Text, textBoxUserName.Text, textBoxPassword.Text);

					cMsg = oclsBusinessServiceVb.CreateBsClassFilesVbV2(chklstSchemaTables);

					txtNotePad.Text = "";
					txtNotePad.Text = oclsBusinessServiceVb.output;

					Clipboard.Clear();						//Clear if any old value is there in Clipboard        
					Clipboard.SetText(txtNotePad.Text);		//Copy text to Clipboard
				}
			}
			
			MessageBox.Show(cMsg, "Data layer helper", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void tabControl1_MouseClick(object sender, MouseEventArgs e)
		{
			if (tabControl1.SelectedIndex == 3)
			{
				getSPCheckListBox();
			}
			else if (tabControl1.SelectedIndex == 1)
			{
				getSchemaViewCheckListBox();
			}
			else if (tabControl1.SelectedIndex == 0)
			{
				getSchemaTableCheckListBox();
			}
			else if (tabControl1.SelectedIndex == 2)
			{
				getSchemaTableCheckListBox();
			}
		}

		


		//private void TabSelected(object sender, System.EventArgs e)
		//{

		//    if (tabControl1.SelectedIndex == 2)
		//    {
		//        getSPCheckListBox();
		//    }
		//    else
		//    {
		//        getSchemaTableCheckListBox();
		//    }
		//}

	}
}
