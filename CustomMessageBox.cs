using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class CustomMessageBox : Window
{
    public CustomMessageBox(string message, string title)
    {
        InitializeComponent(message, title);
    }

    private void InitializeComponent(string message, string title)
    {
        this.Title = title;
        this.SizeToContent = SizeToContent.WidthAndHeight;
        this.Background = Brushes.Red;

        Grid grid = new Grid();
        grid.Margin = new Thickness(10);

        TextBlock textBlock = new TextBlock();
        textBlock.Text = message;
        textBlock.Foreground = Brushes.Black;
        textBlock.Margin = new Thickness(0, 0, 0, 10);

        Button okButton = new Button();
        okButton.Content = "OK";
        okButton.Background = Brushes.Gray;
        okButton.Foreground = Brushes.White;
        okButton.Margin = new Thickness(0, 10, 10, 0);
        okButton.Click += (sender, e) => this.Close();

        grid.RowDefinitions.Add(new RowDefinition());
        grid.RowDefinitions.Add(new RowDefinition());

        Grid.SetRow(textBlock, 0);
        Grid.SetRow(okButton, 1);

        grid.Children.Add(textBlock);
        grid.Children.Add(okButton);

        this.Content = grid;
    }
}
