using System;

namespace SuperVeigar
{
    public class PopupButtonCommand
    {
        protected Action command;

        public PopupButtonCommand(Action command)
        {
            this.command = command;
        }

        public void Excute()
        {
            command?.Invoke();
        }
    }
}