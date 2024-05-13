using UnityEngine;

public class CollectState : IState
{
    int randomNum;
    public void OnEnter(BotController bot)
    {
        randomNum = Random.Range(1, 10);
    }

    public void OnExecute(BotController bot)
    {
        if(bot.myBricks.Count < randomNum)
        {
            if(!bot.agent.pathPending && bot.agent.remainingDistance < 0.1f)
            {
                bot.OnCollecting();
            }
        }
        else
        {
            bot.ChangeState(new ClimpState());
        }
    }

    public void OnExit(BotController bot)
    {
        
    }
}
