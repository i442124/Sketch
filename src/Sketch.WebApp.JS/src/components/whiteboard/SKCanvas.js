import React, { useState, useEffect } from "react";
import { v4 as uuidv4 } from 'uuid';

import { Subscription } from "../../services/SubscriptionService";
import { SKCanvas2DContext } from "./SKCanvas2DContext";

export default function SKCanvas(props) {

  const [action, setAction] = useState('');
  const [painting, setPainting] = useState(false);
  const [position, setPosition] = useState({ x: 0, y: 0 });
  const [context] = useState(() => { return new SKCanvas2DContext(); });

  useEffect(() => {

    Subscription.on('Ink.Stroke', context.stroke);
    Subscription.on('Ink.Wipe', context.wipe);
    Subscription.on('Ink.Fill', context.fill);
    Subscription.on('Clear', context.clear);

    return function cleanup() {
      Subscription.off('Ink.Stroke', context.stroke);
      Subscription.off('Ink.Wipe', context.wipe);
      Subscription.off('Ink.Fill', context.fill);
      Subscription.off('Clear', context.clear);
    }
  }, [context]);

  function onMouseUp(e) {
    if (painting && !isPrimaryButtonPressed(e)) {
      setAction(uuidv4());
      setPainting(false);
    }
  }

  function onMouseDown(e) {

    var currentX = e.nativeEvent.offsetX;
    var currentY = e.nativeEvent.offsetY;

    var previousX = position.x;
    var previousY = position.y;

    if (!painting && isPrimaryButtonPressed(e)) {
      setPainting(true);
      setAction(uuidv4());
      console.log('draw');
      draw(previousX, previousY, currentX, currentY);
    }
  }

  function onMouseMove(e) {

    var currentX = e.nativeEvent.offsetX;
    var currentY = e.nativeEvent.offsetY;

    var previousX = position.x;
    var previousY = position.y;

    setPosition({ x: currentX, y: currentY });
    if (painting && isPrimaryButtonPressed(e)) {
      draw(previousX, previousY, currentX, currentY);
    }
  }

  function draw(previousX, previousY, currentX, currentY) {
    var stroke = {
      style: {
        color: props.color,
        thickness: props.thickness
      },
      stylusPoints: [
        { x: previousX, y: previousY },
        { x: currentX, y: currentY }
      ]
    }

    context.stroke(stroke);
    Subscription.sendToMethod('whiteboard', 'stroke', {
      ...stroke, actionId: action
    });
  }

  function isPrimaryButtonPressed(e) {
    return e.buttons > 0 && ((e.buttons | 1) === 1);
  }

  return (
    <canvas
      width={props.width}
      height={props.height}
      ref={(canvas) => { canvas && context.init(canvas.getContext('2d')); }}
      onMouseMove={(mouseEventArgs) => onMouseMove(mouseEventArgs)}
      onMouseDown={(mouseEventArgs) => onMouseDown(mouseEventArgs)}
      onMouseUp={(mouseEventArgs) => onMouseUp(mouseEventArgs)}>

    </canvas>
  );

}