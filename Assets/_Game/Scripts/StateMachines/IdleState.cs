public class IdleState : IState
{
    public void OnEnter(BotController bot)
    {
        bot.OnIdle();
    }

    public void OnExecute(BotController bot)
    {
        if(bot.targetBricks.Count > 0)
        {
            bot.ChangeState(new CollectState());
        }
    }

    public void OnExit(BotController bot)
    {
        
    }
}
