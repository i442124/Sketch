import { useState } from 'react';
import './App.css';


import SKCanvas from './components/whiteboard/SKCanvas';
import SKColorPicker from './components/toolsettings/SKColorPicker';
import SKBrushSizeSlider from './components/toolsettings/SKBrushSizeSlider';
import SKChannelSelection from './components/subscriptions/SKChannelSelection';

function App() {

  const [thickness, setThickness] = useState(0);
  const [color, setColor] = useState({ r: 0, g: 0, b: 0 });

  return (
    <div className="container-fluid">
      <div className="row no-gutters">
        <div className="col py-2">
          <div className="row no-gutters">
            <label>Groups:</label>
          </div>
          <div className="row no-gutters">
            <SKChannelSelection />
          </div>
        </div>
      </div>

      <div className="row no-gutters">
        <div className="col py-2">
          <div className="row no-gutters">
            <label>Colors:</label>
          </div>
          <div className="row no-gutters">
            <SKColorPicker 
              color={color}
              colorSelected={(color) => setColor(color)} />
          </div>
        </div>
      </div>

      <div className="row no-gutters">
        <div className="col py-2">
          <div className="row no-gutters">
            <label>Thickness:</label>
          </div>
          <div className="row no-gutters">
            <SKBrushSizeSlider
              minValue={1}
              maxValue={35}
              thickness={thickness}
              thicknessSelected={(thickness) => setThickness(thickness)} />
          </div>
        </div>
      </div>

      <div className="row no-gutters">
        <div className="col py-2">
          <div className="row no-gutters">
            <label>Canvas:</label>
          </div>
          <div className="row no-gutters">
            <div className="col-auto border">
              <SKCanvas width={640} height={480} thickness={thickness} color={color}>
              </SKCanvas>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
