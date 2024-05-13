public class WinState : IState
{
    public void OnEnter(BotController bot)
    {
       
    }

    public void OnExecute(BotController bot)
    {
        bot.OnWining();
    }

    public void OnExit(BotController bot)
    {
       
    }
}
