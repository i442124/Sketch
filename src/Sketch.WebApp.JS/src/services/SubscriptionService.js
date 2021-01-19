import { HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";

const ApplicationBaseAddress = "https://localhost:5001";

class SubscriptionService {

  constructor() {

    this.connection = new HubConnectionBuilder()
      .withUrl(`${ApplicationBaseAddress}/hub`)
      .withAutomaticReconnect()
      .build();
  }

  async subscribe(channel) {
    if (this.connection.state !== HubConnectionState.Connected) {
      await this.connection.start();
    }

    await fetch(`${ApplicationBaseAddress}/api/subscribe/${this.connection.connectionId}/${channel}`);
  }

  async unsubscribe(channel) {
    if (this.connection.state !== HubConnectionState.Connected) {
      await this.connection.start();
    }

    await fetch(`${ApplicationBaseAddress}/api/unsubscribe/${this.connection.connectionId}/${channel}`);
  }

  async send(methodGroup, data) {
    return await fetch(`${ApplicationBaseAddress}/api/${methodGroup}/${this.connection.connectionId}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    })
  }

  async sendToMethod(methodGroup, methodName, data) {
    return await fetch(`${ApplicationBaseAddress}/api/${methodGroup}/${methodName}/${this.connection.connectionId}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    });
  }

  on(eventType, eventHandler) {
    console.log(`on - Sketch.Shared.Data.${eventType}`);
    this.connection.on(`Sketch.Shared.Data.${eventType}`, eventHandler);
  }

  off(eventType, eventHandler) {
    console.log(`off - Sketch.Shared.Data.${eventType}`);
    this.connection.off(`Sketch.Shared.Data.${eventType}`, eventHandler);
  }
}

export const Subscription = new SubscriptionService();
