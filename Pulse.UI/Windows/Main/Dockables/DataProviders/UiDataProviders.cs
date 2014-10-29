﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Pulse.UI
{
    public sealed class UiDataProviders : UiMainDockableControl
    {
        private readonly UiListView _listView;

        public UiDataProviders()
        {
            #region Construct

            Header = "Источники данных";

            _listView = UiListViewFactory.Create();
            _listView.ItemTemplate = CreateTemplate();
            _listView.ItemContainerStyle = CreateNodeStyle();

            Content = _listView;

            #endregion

            Refresh();
            //Loaded += OnLoaded;
        }

        private Style CreateNodeStyle()
        {
            Style style = new Style();
            style.Setters.Add(new Setter(ContextMenuProperty, new Binding("ContextMenu") {Mode = BindingMode.TwoWay}));
            return style;
        }

        private static DataTemplate CreateTemplate()
        {
            DataTemplate template = new DataTemplate {DataType = typeof(UiDataProviderNode)};

            FrameworkElementFactory stackPanel = new FrameworkElementFactory(typeof(StackPanel));
            stackPanel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory image = new FrameworkElementFactory(typeof(Image));
            image.SetValue(HeightProperty, 16d);
            image.SetValue(Image.SourceProperty, new Binding("Icon"));
            image.SetValue(MarginProperty, new Thickness(3));
            stackPanel.AppendChild(image);

            FrameworkElementFactory textBlock = new FrameworkElementFactory(typeof(TextBlock));
            textBlock.SetBinding(TextBlock.TextProperty, new Binding("Title"));
            textBlock.SetBinding(ToolTipProperty, new Binding("Description"));
            textBlock.SetValue(MarginProperty, new Thickness(3));
            stackPanel.AppendChild(textBlock);

            template.VisualTree = stackPanel;

            return template;
        }

        private void Refresh()
        {
            try
            {
                _listView.ItemsSource = new[]
                {
                    UiDataProviderNode.Create(InteractionService.Configuration),
                    UiDataProviderNode.Create(InteractionService.GameLocation),
                    UiDataProviderNode.Create(InteractionService.WorkingLocation)
                };
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        protected override int Index
        {
            get { return 0; }
        }
    }
}