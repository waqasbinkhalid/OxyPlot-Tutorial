using OxyPlot_Tutorial.View;
using OxyPlot_Tutorial.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OxyPlot_Tutorial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RunTime();

            //StackOverflow();

            //TrigonometricPlotDemo();

            //RefreshDemo();

            //OxyPlotDemo();

            //RealtimeDemo();
        }

        private void TrigonometricPlotDemo()
        {
            TrigonometricPlotView view = new TrigonometricPlotView();
            TrigonometricPlotViewModel viewModel = new TrigonometricPlotViewModel();

            view.DataContext = viewModel;
            this.placeHolder.Content = view;
        }

        private void OxyPlotDemo()
        {
            OxyPlotDemoView view = new OxyPlotDemoView();
            OxyPlotDemoViewModel viewModel = new OxyPlotDemoViewModel();

            view.DataContext = viewModel;
            this.placeHolder.Content = view;
        }

        private void RealtimeDemo()
        {
            RealtimeDemoView view = new RealtimeDemoView();
            RealtimeDemoViewModel viewModel = new RealtimeDemoViewModel();

            view.DataContext = viewModel;
            this.placeHolder.Content = view;
        }
         
        private void RefreshDemo()
        {
            RefreshDemoView view = new RefreshDemoView();
            RefreshDemoViewModel viewModel = new RefreshDemoViewModel();

            view.DataContext = viewModel;
            this.placeHolder.Content = view;
        }

        private void StackOverflow()
        {
            StackOverflowView view = new StackOverflowView();
            StackOverflowViewModel viewModel = new StackOverflowViewModel();

            view.DataContext = viewModel;
            this.placeHolder.Content = view;
        }

        private void RunTime()
        {
            RunTimeView view = new RunTimeView();
            RunTimeViewModel viewModel = new RunTimeViewModel();

            view.DataContext = viewModel;
            this.placeHolder.Content = view;
        }
    }
}
