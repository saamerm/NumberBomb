using Sharpnado.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NumberBomb.Behaviours
{
  public class TabSelectedIndexBehaviour : Behavior<TabHostView>
  {
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(TabSelectedIndexBehaviour), null);
    public static readonly BindableProperty EventNameProperty = BindableProperty.Create<TabSelectedIndexBehaviour, string>(p => p.EventName, null);
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<TabSelectedIndexBehaviour, object>(p => p.CommandParameter, null);
    public TabHostView Bindable { get; private set; }
    public string EventName
    {
      get { return (string)GetValue(EventNameProperty); }
      set { SetValue(EventNameProperty, value); }
    }
    public object CommandParameter
    {
      get { return GetValue(CommandParameterProperty); }
      set { SetValue(CommandParameterProperty, value); }
    }
    public ICommand Command
    {
      get { return (ICommand)GetValue(CommandProperty); }
      set { SetValue(CommandProperty, value); }
    }
    protected override void OnAttachedTo(TabHostView bindable)
    {
      base.OnAttachedTo(bindable);
      Bindable = bindable;
      Bindable.BindingContextChanged += OnBindingContextChanged;
      Bindable.SelectedTabIndexChanged += OnCheckedChanged;
    }

    private void OnCheckedChanged(object sender, SelectedPositionChangedEventArgs e)
    {
      Command?.Execute(sender);
    }

    protected override void OnDetachingFrom(TabHostView bindable)
    {
      base.OnDetachingFrom(bindable);
      Bindable.BindingContextChanged -= OnBindingContextChanged;
      Bindable.SelectedTabIndexChanged -= OnCheckedChanged;
      Bindable = null;
    }
    private void OnBindingContextChanged(object sender, EventArgs e)
    {
      OnBindingContextChanged();
      BindingContext = Bindable.BindingContext;
    }
  }
}

