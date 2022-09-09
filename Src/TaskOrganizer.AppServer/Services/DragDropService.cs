using TaskOrganizer.AppServer.DragDrop;

namespace TaskOrganizer.AppServer.Services
{
    public class DragDropService : IDragDropService
    {
        public IDragDropHandler? DragHandler { get; set; }
        public IDragDropHandler? DropHandler { get; set; }

        public void StartDrag(IDragDropHandler dragHandler)
        {
            DragHandler = dragHandler;
        }

        public void SetDropTarget(IDragDropHandler dropHandler)
        {
            DropHandler = dropHandler;

            if (DragHandler?.PayLoad is not null)
                DropHandler.PayLoad = DragHandler.PayLoad;
        }

        public void RemoveDropTarget()
        {
            DropHandler = null;
        }

        public async Task HandleDropAsync()
        {
            if (DragHandler is null || DropHandler is null)
                return;

            await DragHandler.HandleDropAsync();
            await DropHandler.HandleDropAsync();

            Reset();
        }

        public bool Accepts(IDragDropHandler dropHandler)
        {
            return DragHandler?.Zone == dropHandler.Zone;
        }

        private void Reset()
        {
            DragHandler = null;
            DropHandler = null;
        }
    }
}
