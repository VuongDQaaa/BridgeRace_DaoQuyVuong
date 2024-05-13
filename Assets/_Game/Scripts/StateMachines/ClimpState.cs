public class ClimpState : IState
{

    public void OnEnter(BotController bot)
    {

    }

    public void OnExecute(BotController bot)
    {
        if(bot.myBricks.Count > 0)
        {
            bot.OnClimping();
        }
        else if(bot.myBricks.Count <= 0 && bot.isFinished == false)
        {
            bot.agent.ResetPath();
            bot.ChangeState(new CollectState());
        }
        else
        {
            bot.ChangeState(new WinState());
        }
    }

    public void OnExit(BotController bot)
    {
        
    }
}
