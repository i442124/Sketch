using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class WhiteboardModel : IWhiteboardModel
    {
        private readonly ISubscriptionModel _subscription;
        private readonly ISubscriptionEventModel<Fill> _fillEvent;
        private readonly ISubscriptionEventModel<Wipe> _wipeEvent;
        private readonly ISubscriptionEventModel<Stroke> _strokeEvent;
        private readonly ISubscriptionEventModel<Clear> _clearEvent;
        private readonly ISubscriptionEventModel<Undo> _undoEvent;

        public string ActionId { get; private set; }

        public WhiteboardModel(
            ISubscriptionModel subscription,
            ISubscriptionEventModel<Stroke> strokeEvent,
            ISubscriptionEventModel<Wipe> wipeEvent,
            ISubscriptionEventModel<Fill> fillEvent,
            ISubscriptionEventModel<Clear> clearEvent,
            ISubscriptionEventModel<Undo> undoEvent)
        {
            _subscription = subscription;
            _subscription.OnReceive<StrokeEvent>(ReceiveAsync);
            _subscription.OnReceive<ClearEvent>(ReceiveAsync);
            _subscription.OnReceive<WipeEvent>(ReceiveAsync);
            _subscription.OnReceive<FillEvent>(ReceiveAsync);
            _subscription.OnReceive<UndoEvent>(ReceiveAsync);

            _strokeEvent = strokeEvent;
            _wipeEvent = wipeEvent;
            _fillEvent = fillEvent;
            _clearEvent = clearEvent;
            _undoEvent = undoEvent;
        }

        public async Task StrokeAsync(Stroke stroke)
        {
            stroke.ActionId = ActionId;
            await _strokeEvent.InvokeAsync(stroke);
            await _subscription.SendAsync("whiteboard", "stroke", stroke);
        }

        public async Task WipeAsync(Wipe wipe)
        {
            wipe.ActionId = ActionId;
            await _wipeEvent.InvokeAsync(wipe);
            await _subscription.SendAsync("whiteboard", "wipe", wipe);
        }

        public async Task FillAsync(Fill fill)
        {
            fill.ActionId = ActionId;
            await _fillEvent.InvokeAsync(fill);
            await _subscription.SendAsync("whiteboard", "fill", fill);
        }

        public async Task ClearAsync(Clear clear)
        {
            clear.ActionId = ActionId;
            await _clearEvent.InvokeAsync(clear);
            await _subscription.SendAsync("whiteboard", "clear", clear);
        }

        public async Task UndoAsync(Undo undo)
        {
            await _undoEvent.InvokeAsync(undo);
            await _subscription.SendAsync("whiteboard", "undo", undo);
        }

        public IDisposable OnReceive(Func<Stroke, Task> handler)
        {
            return _strokeEvent.OnInvokeAsync(handler);
        }

        public IDisposable OnReceive(Func<Wipe, Task> handler)
        {
            return _wipeEvent.OnInvokeAsync(handler);
        }

        public IDisposable OnReceive(Func<Fill, Task> handler)
        {
            return _fillEvent.OnInvokeAsync(handler);
        }

        public IDisposable OnReceive(Func<Clear, Task> handler)
        {
            return _clearEvent.OnInvokeAsync(handler);
        }

        public IDisposable OnReceive(Func<Undo, Task> handler)
        {
            return _undoEvent.OnInvokeAsync(handler);
        }

        public async Task InvokeActionChanged()
        {
            await Task.Run(() => ActionId = Guid.NewGuid().ToString());
        }

        private async Task ReceiveAsync(StrokeEvent strokeEvent)
        {
            await _strokeEvent.InvokeAsync(strokeEvent.Stroke);
        }

        private async Task ReceiveAsync(WipeEvent wipeEvent)
        {
            await _wipeEvent.InvokeAsync(wipeEvent.Wipe);
        }

        private async Task ReceiveAsync(FillEvent fillEvent)
        {
            await _fillEvent.InvokeAsync(fillEvent.Fill);
        }

        private async Task ReceiveAsync(ClearEvent clearEvent)
        {
            await _clearEvent.InvokeAsync(clearEvent.Clear);
        }

        private async Task ReceiveAsync(UndoEvent undoEvent)
        {
            await _undoEvent.InvokeAsync(undoEvent.Undo);
        }
    }
}
