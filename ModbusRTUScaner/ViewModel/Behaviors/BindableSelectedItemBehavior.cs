using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace ModbusRTUScanner.ViewModel.Behaviors
{
    public class BindableSelectedItemBehavior : Behavior<TreeView>
    {
        #region SelectedItem Property

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(BindableSelectedItemBehavior),
                new UIPropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as BindableSelectedItemBehavior;
            if (behavior?.AssociatedObject != null)
            {
                var item = e.NewValue as TreeViewItem;
                if (item != null)
                {
                    item.SetValue(TreeViewItem.IsSelectedProperty, true);
                }
            }
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
            this.AssociatedObject.PreviewMouseDown += OnTreeViewPreviewMouseDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
                this.AssociatedObject.PreviewMouseDown -= OnTreeViewPreviewMouseDown;
            }
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.SelectedItem = e.NewValue;
        }

        private void OnTreeViewPreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var treeView = sender as TreeView;
            if (treeView != null)
            {
                var hitTestResult = System.Windows.Media.VisualTreeHelper.HitTest(treeView, e.GetPosition(treeView));
                if (hitTestResult == null || hitTestResult.VisualHit is System.Windows.Media.Visual visual &&
                    !IsChildOfTreeViewItem(visual))
                {
                    // Если клик был вне элементов TreeView, сбрасываем SelectedItem
                    this.SelectedItem = null;
                }
                else
                {
                    // Если клик был по элементу TreeView, принудительно обновляем SelectedItem
                    var treeViewItem = FindParentTreeViewItem(hitTestResult.VisualHit);
                    if (treeViewItem != null)
                    {
                        this.SelectedItem = treeViewItem.DataContext;
                    }
                }
            }
        }

        private bool IsChildOfTreeViewItem(System.Windows.Media.Visual visual)
        {
            var parent = System.Windows.Media.VisualTreeHelper.GetParent(visual);
            while (parent != null)
            {
                if (parent is TreeViewItem)
                    return true;
                parent = System.Windows.Media.VisualTreeHelper.GetParent(parent);
            }
            return false;
        }

        private TreeViewItem FindParentTreeViewItem(System.Windows.DependencyObject element)
        {
            while (element != null)
            {
                if (element is TreeViewItem treeViewItem)
                    return treeViewItem;
                element = System.Windows.Media.VisualTreeHelper.GetParent(element);
            }
            return null;
        }
    }
}