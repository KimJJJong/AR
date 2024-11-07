using System;

public abstract class BehaviorNode
{
    public abstract bool Execute();
}

public class SelectorNode : BehaviorNode
{
    private BehaviorNode[] children;

    public SelectorNode(params BehaviorNode[] children)
    {
        this.children = children;
    }

    public override bool Execute()
    {
        foreach (var child in children)
        {
            if (child.Execute()) // 성공하면 종료
                return true;
        }
        return false; // 모두 실패하면 실패
    }
}

public class SequenceNode : BehaviorNode
{
    private BehaviorNode[] children;

    public SequenceNode(params BehaviorNode[] children)
    {
        this.children = children;
    }

    public override bool Execute()
    {
        foreach (var child in children)
        {
            if (!child.Execute()) // 하나라도 실패하면 실패
                return false;
        }
        return true; // 모두 성공하면 성공
    }
}

public class ActionNode : BehaviorNode
{
    private Func<bool> action;

    public ActionNode(Func<bool> action)
    {
        this.action = action;
    }

    public override bool Execute()
    {
        return action();
    }
}

public class ConditionNode : BehaviorNode
{
    private Func<bool> condition;

    public ConditionNode(Func<bool> condition)
    {
        this.condition = condition;
    }

    public override bool Execute()
    {
        return condition();
    }
}
