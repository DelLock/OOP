using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageEditor
{
    public class HistoryManager
    {
        private Stack<Bitmap> undoStack;
        private Stack<Bitmap> redoStack;
        private const int MAX_HISTORY = 20;

        public bool CanUndo => undoStack.Count > 0;
        public bool CanRedo => redoStack.Count > 0;

        public HistoryManager()
        {
            undoStack = new Stack<Bitmap>();
            redoStack = new Stack<Bitmap>();
        }

        public void PushState(Bitmap state)
        {
            if (undoStack.Count >= MAX_HISTORY)
            {
                undoStack.Reverse();
                undoStack.Pop();
                undoStack.Reverse();
            }

            undoStack.Push(new Bitmap(state));
            redoStack.Clear();
        }

        public Bitmap Undo(Bitmap current)
        {
            if (CanUndo)
            {
                redoStack.Push(new Bitmap(current));
                return new Bitmap(undoStack.Pop());
            }
            return current;
        }

        public Bitmap Redo(Bitmap current)
        {
            if (CanRedo)
            {
                undoStack.Push(new Bitmap(current));
                return new Bitmap(redoStack.Pop());
            }
            return current;
        }

        public void Clear()
        {
            undoStack.Clear();
            redoStack.Clear();
        }
    }
}