﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParvazPardaz.Resource.Link {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class LinkResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LinkResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ParvazPardaz.Resource.Link.LinkResource", typeof(LinkResource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to country-city.
        /// </summary>
        public static string LastSecondTourUrlPattern {
            get {
                return ResourceManager.GetString("LastSecondTourUrlPattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Link Management.
        /// </summary>
        public static string LinkManagement {
            get {
                return ResourceManager.GetString("LinkManagement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to country-city.
        /// </summary>
        public static string LinkTableTitlePattern {
            get {
                return ResourceManager.GetString("LinkTableTitlePattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;p&gt;if country : &lt;strong&gt;/tour-country/&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;if city : &lt;strong&gt;/tour-country-city/&lt;/strong&gt;&lt;/p&gt;.
        /// </summary>
        public static string LocationLandingPageUrlPattern {
            get {
                return ResourceManager.GetString("LocationLandingPageUrlPattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to لطفا URL را مطابق الگوی زیر ، با استفاده از کلمات انگلیسی و (نگارش صحیح) بسازید.
        /// </summary>
        public static string UseThisLinkTableTitlePattern {
            get {
                return ResourceManager.GetString("UseThisLinkTableTitlePattern", resourceCulture);
            }
        }
    }
}