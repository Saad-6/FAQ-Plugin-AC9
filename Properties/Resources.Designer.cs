﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FAQPlugin.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FAQPlugin.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SET ANSI_NULLS ON
        ///GO
        ///
        ///SET QUOTED_IDENTIFIER ON
        ///GO
        ///
        ///-- Create the Settings table with a single row constraint
        ///CREATE TABLE [dbo].[FAQSettings] (
        ///    [Id] [int] NOT NULL PRIMARY KEY CHECK ([Id] = 1), -- Ensures only one row
        ///    [AllowAnonymousUsersToAnswerQuestions] [bit] NOT NULL DEFAULT 0,
        ///     
        ///) ON [PRIMARY]
        ///GO
        ///
        ///-- Insert the default settings row
        ///INSERT INTO [dbo].[FAQSettings] (Id, AllowAnonymousUsersToAnswerQuestions, DefaultResponderName)
        ///VALUES (1, 0, &apos;Admin&apos;)
        ///GO
        ///.
        /// </summary>
        internal static string create_faq_settings {
            get {
                return ResourceManager.GetString("create_faq_settings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SET ANSI_NULLS ON
        ///GO
        ///
        ///SET QUOTED_IDENTIFIER ON
        ///GO
        ///
        ///CREATE TABLE [dbo].[FAQ](
        ///    [Id] [int] IDENTITY(1,1) ,
        ///    [Question] [nvarchar](255) NOT NULL,
        ///    [Answer] [nvarchar](255) NULL,
        ///    [UserId] [int] NULL,
        ///    [CreatedDate] [datetime] NULL,
        ///    [ProductId] [int] NOT NULL,
        ///    [IsAnswered] [bit] NULL,
        ///    [Visibility] [bit] NULL,
        /// CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
        ///(
        ///    [Id] ASC
        ///)WITH ( IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ///) ON [PRIMAR [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string create_faq_table {
            get {
                return ResourceManager.GetString("create_faq_table", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DROP TABLE [dbo].[FAQ]
        ///DROP TABLE [dbo].[FAQSettings].
        /// </summary>
        internal static string drop_faq_table {
            get {
                return ResourceManager.GetString("drop_faq_table", resourceCulture);
            }
        }
    }
}
