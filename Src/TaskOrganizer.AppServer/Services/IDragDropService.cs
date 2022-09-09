using TaskOrganizer.AppServer.DragDrop;

namespace TaskOrganizer.AppServer.Services
{
    public interface IDragDropService
    {
        bool Accepts(IDragDropHandler dropHandler);
        Task HandleDropAsync();
        void RemoveDropTarget();
        void SetDropTarget(IDragDropHandler dropHandler);
        void StartDrag(IDragDropHandler dragHandler);
    }
}