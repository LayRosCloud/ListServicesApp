// Updated by XamlIntelliSenseFileGenerator 03.06.2022 1:22:22
#pragma checksum "..\..\RegistrationWIndow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E54FA19F16C0DB3109A16A4288F79BE67E5516F60B4F77A45808222259D036A1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ApplicationForBD;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ApplicationForBD
{


    /// <summary>
    /// RegistrationWIndow
    /// </summary>
    public partial class RegistrationWIndow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 26 "..\..\RegistrationWIndow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitButton;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ApplicationForBD;component/registrationwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\RegistrationWIndow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 9 "..\..\RegistrationWIndow.xaml"
                    ((ApplicationForBD.RegistrationWIndow)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);

#line default
#line hidden
                    return;
                case 2:
                    this.ExitButton = ((System.Windows.Controls.Button)(target));

#line 28 "..\..\RegistrationWIndow.xaml"
                    this.ExitButton.Click += new System.Windows.RoutedEventHandler(this.ExitButton_Click);

#line default
#line hidden
                    return;
                case 3:
                    this.passwordPassBox = ((System.Windows.Controls.PasswordBox)(target));
                    return;
                case 4:
                    this.passwordTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 5:
                    this.VisiblePassword = ((System.Windows.Controls.Button)(target));

#line 160 "..\..\RegistrationWIndow.xaml"
                    this.VisiblePassword.Click += new System.Windows.RoutedEventHandler(this.VisiblePassword_Click);

#line default
#line hidden
                    return;
                case 6:
                    this.eyeImage = ((System.Windows.Controls.Image)(target));
                    return;
                case 7:
                    this.VisibleClosePassword = ((System.Windows.Controls.Button)(target));

#line 163 "..\..\RegistrationWIndow.xaml"
                    this.VisibleClosePassword.Click += new System.Windows.RoutedEventHandler(this.VisiblePassword_Click);

#line default
#line hidden
                    return;
                case 8:
                    this.eyeCloseImage = ((System.Windows.Controls.Image)(target));
                    return;
                case 9:
                    this.AutorizationButton = ((System.Windows.Controls.Button)(target));

#line 220 "..\..\RegistrationWIndow.xaml"
                    this.AutorizationButton.Click += new System.Windows.RoutedEventHandler(this.AutorizationButton_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Frame frameMain;
    }
}

