import React from "react";

export default function SKBrushSizeSlider(props) {

  function setThickness(thickness) {
    console.log(thickness);
    const { thicknessSelected } = props;
    !!thicknessSelected && thicknessSelected(thickness);
  }

  return (
    
    <div className="row no-gutters">
      <input type="range" className="mr-1"
        min={props.minValue} max={props.maxValue} 
        onChange={(__e) => setThickness(__e.target.value)}/>

      <label>{props.thickness}</label>
    </div>
  );
}
