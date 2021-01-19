import React from "react";

export default function SKColorPicker(props) {

  function selectColor(color) {

    const { colorSelected } = props;
    !!colorSelected && colorSelected(color);
  }

  return (
    <div className="d-flex flex-row">
      <button className="btn btn-danger mx-2" onClick={() => selectColor({r:220, g: 53, b: 69})}>R</button>
      <button className="btn btn-success mx-2" onClick={() => selectColor({r:40, g: 167, b: 69})}>G</button>
      <button className="btn btn-primary mx-2" onClick={() => selectColor({r:0, g: 123, b: 255})}>B</button>
    </div>
  );
}