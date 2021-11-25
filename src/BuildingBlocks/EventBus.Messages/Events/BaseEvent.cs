namespace EventBus.Messages.Events
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public BaseEvent(Guid id, DateTime createdDate)
        {
            Id = id;
            CreatedDate = createdDate;
        }

        public Guid Id { get; private set; }

        public DateTime CreatedDate { get; private set; }
    }
}
