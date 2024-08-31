using CommerceBuilder.Common;
using CommerceBuilder.Plugins;
using CommerceBuilder.Utility;
using FAQPlugin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace FAQPlugin
{
    public class FAQPlugin : PluginBase
    {
        public FAQPlugin(PluginManifest manifest)
            :base(manifest)
        {
        }
        public override bool Install()
        {
            bool installed = false;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["AbleCommerce"].ConnectionString;
                var errors = RunScript(connectionString, Properties.Resources.create_faq_table);
                if (errors.Count > 0 )
                {
                    Logger.Error(string.Format("There are errors when trying to create the faq table '{0}', please change log level to info for error details.", this.Manifest.Name));
                    errors.ForEach(e => Logger.Info(e));
                   
                }
                else
                {
                    var currentStoreSettings = AbleContext.Current.Store.Settings;
                    var defaultSettings = new FAQSettings();
                    currentStoreSettings.SetValueByKey("FAQ_AllowAnonymousUsers", defaultSettings.AllowAnonymousUsers.ToString());
                    currentStoreSettings.SetValueByKey("FAQ_DefaultResponderName", defaultSettings.DefaultResponderName);
                    currentStoreSettings.Save();
                    installed = true;  
                }

            }
            catch (SqlException e )
            {
                Logger.Error(e.Message, e);
            }
            return installed;
        }
        public override bool UnInstall()
        {
            
            bool uninstalled = false;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["AbleCommerce"].ConnectionString;
                var errors = RunScript(connectionString, Properties.Resources.drop_faq_table);
                if (errors.Count > 0)
                {
                    Logger.Error(string.Format("There are errors when trying to uninstall '{0}', please change log level to info for error details.", this.Manifest.Name));
                    errors.ForEach(e => Logger.Info(e));
                }
                else
                {
                    uninstalled = true;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
            }

            return uninstalled;
        }

        private List<string> RunScript(string connectionString, string sqlScript)
        {
            // initialize the error list
            List<string> errorList = new List<string>();

            // REMOVE SCRIPT COMMENTS
            sqlScript = Regex.Replace(sqlScript, @"/\*.*?\*/", string.Empty);

            // SPLIT INTO COMMANDS
            string[] commands = StringHelper.Split(sqlScript, "\r\nGO\r\n");

            // SETUP DATABASE CONNECTION
            int errorCount = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Run each command on the database
                conn.Open();
                foreach (string sql in commands)
                {
                    if (!string.IsNullOrEmpty(sql.Trim().Trim('\r', '\n')))
                    {
                        try
                        {
                            SqlCommand command = new SqlCommand(sql, conn);
                            command.CommandTimeout = 0;
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            errorCount++;
                            errorList.Add("<b>SQL:</b> " + sql);
                            errorList.Add("<b>Error:</b> " + ex.Message);
                        }
                    }
                }
                conn.Close();
            }
            if (errorCount > 0) errorList.Add("<b>Errors Count:</b> " + errorCount);

            return errorList;
        }



    }
}
