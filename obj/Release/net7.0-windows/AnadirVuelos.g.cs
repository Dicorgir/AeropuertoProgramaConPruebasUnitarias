﻿#pragma checksum "..\..\..\AnadirVuelos.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A556E7ACD9EFEB3E18EE67D7467D6AE201CDB446"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Login;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Login {
    
    
    /// <summary>
    /// AnadirVuelos
    /// </summary>
    public partial class AnadirVuelos : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\AnadirVuelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtOrigen;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\AnadirVuelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDestino;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\AnadirVuelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSalida;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\AnadirVuelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtLlegada;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\AnadirVuelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAceptar;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\AnadirVuelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLimpiar;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\AnadirVuelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboIdAvion;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\AnadirVuelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DatePickerFecha;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Login;component/anadirvuelos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AnadirVuelos.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtOrigen = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtDestino = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtSalida = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtLlegada = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btnAceptar = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\AnadirVuelos.xaml"
            this.btnAceptar.Click += new System.Windows.RoutedEventHandler(this.btnAceptar_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnLimpiar = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\AnadirVuelos.xaml"
            this.btnLimpiar.Click += new System.Windows.RoutedEventHandler(this.btnLimpiar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ComboIdAvion = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.DatePickerFecha = ((System.Windows.Controls.DatePicker)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

