namespace AISalesAgent.Domain.Enums
{
    public enum TaskState
    {
        PENDING,
        RUNNING,
        WAITING_FOR_CLIENT,
        ESCALATED,
        COMPLETED,
        FAILED,
        CANCELLED
    }
}
