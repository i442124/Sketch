using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Services;

namespace Sketch.Shared.Models
{
    public class WhiteboardClient : IWhiteboardClient
    {
        private readonly ISubscriptionService _subscriptions;
        private readonly INotificationService _notifications;
        private readonly IWhiteboardStorage _storage;

        public string ActionId { get; private set; }

        public IEnumerable<Event> Actions
        {
            get { return _storage.Actions; }
        }

        public WhiteboardClient(
            ISubscriptionService subscriptions,
            INotificationService notifications,
            IWhiteboardStorage storage)
        {
            subscriptions.OnReceive<Stroke>(notifications.InvokeAsync);
            subscriptions.OnReceive<Wipe>(notifications.InvokeAsync);
            subscriptions.OnReceive<Fill>(notifications.InvokeAsync);

            _storage = storage;
            _subscriptions = subscriptions;
            _notifications = notifications;
        }

        public Task InvokeActionChanged()
        {
            return Task.Run(() => ActionId = Guid.NewGuid().ToString());
        }

        public IDisposable OnStroke(Func<Stroke, Task> handler)
        {
            return _notifications.Subscribe(handler);
        }

        public IDisposable OnFill(Func<Fill, Task> handler)
        {
            return _notifications.Subscribe(handler);
        }

        public IDisposable OnWipe(Func<Wipe, Task> handler)
        {
            return _notifications.Subscribe(handler);
        }

        public IDisposable OnClear(Func<Clear, Task> handler)
        {
            return _notifications.Subscribe(handler);
        }

        public IDisposable OnUndo(Func<Undo, Task> handler)
        {
            return _notifications.Subscribe(handler);
        }

        public async Task StrokeAsync(Stroke stroke)
        {
            stroke.ActionId = ActionId;
            await _storage.PushAsync(stroke);
            await _notifications.InvokeAsync(stroke);
            await _subscriptions.SendAsync("whiteboard", "stroke", stroke);
        }

        public async Task FillAsync(Fill fill)
        {
            fill.ActionId = ActionId;
            await _storage.PushAsync(fill);
            await _notifications.InvokeAsync(fill);
            await _subscriptions.SendAsync("whiteboard", "fill", fill);
        }

        public async Task WipeAsync(Wipe wipe)
        {
            wipe.ActionId = ActionId;
            await _storage.PushAsync(wipe);
            await _notifications.InvokeAsync(wipe);
            await _subscriptions.SendAsync("whiteboard", "wipe", wipe);
        }

        public async Task ClearAsync(Clear clear)
        {
            clear.ActionId = ActionId;
            await _storage.PushAsync(clear);
            await _notifications.InvokeAsync(clear);
            await _subscriptions.SendAsync("whiteboard", "clear", clear);
        }

        public async Task UndoAsync(Undo undo)
        {
            ActionId = await _storage.PopAsync();
            await _notifications.InvokeAsync(undo);
            await _subscriptions.SendAsync("whiteboard", "undo", undo);
        }
    }
}
