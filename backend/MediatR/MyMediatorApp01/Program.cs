var textBox = new TextBox();
var checkBox = new CheckBox();
var button = new Button();

var mediator = new DialogMediator
{
    TextBox = textBox,
    CheckBox = checkBox,
    Button = button
};

textBox.SetMediator(mediator);
checkBox.SetMediator(mediator);
button.SetMediator(mediator);

button.Click();
textBox.Input("João");
checkBox.Toggle();
button.Click();
checkBox.Toggle();
button.Click();

public interface IMediator
{
    void Notify(object sender, string @event);
}

public abstract class Component
{
    protected IMediator _mediator;
    public void SetMediator(IMediator mediator) => _mediator = mediator;
}

public class TextBox : Component
{
    public string Text { get; private set; } = string.Empty;

    public void Input(string value)
    {
        Text = value;
        Console.WriteLine($"TextBx: {Text}");
        _mediator.Notify(this, "TextChanged");
    }
}

public class CheckBox : Component
{
    public bool IsChecked { get; private set; }

    public void Toggle()
    {
        IsChecked = !IsChecked;
        Console.WriteLine($"[CheckBox] Marcado: {IsChecked}");
        _mediator.Notify(this, "CheckedChanged");
    }
}

public class Button : Component
{
    public bool IsEnabled { get; set; }

    public void Click()
    {
        if (IsEnabled)
            Console.WriteLine("[Button] Clique processado!");
        else
            Console.WriteLine("[Button] o botão está desabilitado.");

        _mediator.Notify(this, "ButtonChanged");
    }
}


public class DialogMediator : IMediator
{
    public TextBox TextBox { get; set; }
    public CheckBox CheckBox { get; set; }
    public Button Button { get; set; }

    public void Notify(object sender, string @event)
    {
        if (@event == "TextChanged" || @event == "CheckedChanged")
        {
            var enableButton = !string.IsNullOrEmpty(TextBox.Text) && CheckBox.IsChecked;
            Button.IsEnabled = enableButton;
            Console.WriteLine($"[Mediator] Botão {(enableButton ? "habilitado" : "desabilitado")}.");
        }
    }
}