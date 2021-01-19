import React, { useState } from "react";
import { Subscription } from "../../services/SubscriptionService";

export default function SKChannelSelection() {

  const [value, setValue] = useState('');
  async function onSelectionChanged(newValue) {

    if (value && value.length) {
      await Subscription.unsubscribe(value);
    }

    await Subscription.subscribe(newValue);
    setValue(newValue);
  }

  return (
    <select onChange={(__e) => onSelectionChanged(__e.target.value)}>
      <option value="" selected disabled hidden>None</option>
      <option value="abc">Group 1</option>
      <option value="123">Group 2</option>
      <option value="def">Group 3</option>
      <option value="456">Group 4</option>
    </select>
  );
}