﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POS.Infrastructure.Exceptions {
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
    internal class CustomExceptions {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CustomExceptions() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("POS.Infrastructure.Exceptions.CustomExceptions", typeof(CustomExceptions).Assembly);
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
        ///   Looks up a localized string similar to DepartmentNameAlreadyExist.
        /// </summary>
        internal static string DepartmentNameAlreadyExist {
            get {
                return ResourceManager.GetString("DepartmentNameAlreadyExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Department Name.
        /// </summary>
        internal static string InvalidDepartmentName {
            get {
                return ResourceManager.GetString("InvalidDepartmentName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Email.
        /// </summary>
        internal static string InvalidEmail {
            get {
                return ResourceManager.GetString("InvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to InvalidPassword.
        /// </summary>
        internal static string InvalidPassword {
            get {
                return ResourceManager.GetString("InvalidPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Supplier Name.
        /// </summary>
        internal static string InvalidSupplierName {
            get {
                return ResourceManager.GetString("InvalidSupplierName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Department Info Availiable.
        /// </summary>
        internal static string NoDepartmentInfoAvailiable {
            get {
                return ResourceManager.GetString("NoDepartmentInfoAvailiable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Supplier Info Availiable.
        /// </summary>
        internal static string NoSupplierInfoAvailiable {
            get {
                return ResourceManager.GetString("NoSupplierInfoAvailiable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No User Info Availiable.
        /// </summary>
        internal static string NoUserInfoAvailiable {
            get {
                return ResourceManager.GetString("NoUserInfoAvailiable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Success.
        /// </summary>
        internal static string Success {
            get {
                return ResourceManager.GetString("Success", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Supplier Name Already Exists.
        /// </summary>
        internal static string SupplierNameAlreadyExist {
            get {
                return ResourceManager.GetString("SupplierNameAlreadyExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Internal Error has occured. Please contact the administrator! .
        /// </summary>
        internal static string UnknownException {
            get {
                return ResourceManager.GetString("UnknownException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User&apos;s Email Already Exists.
        /// </summary>
        internal static string UserEmailAlreadyExists {
            get {
                return ResourceManager.GetString("UserEmailAlreadyExists", resourceCulture);
            }
        }
    }
}
