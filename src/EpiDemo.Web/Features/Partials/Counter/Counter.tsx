import React, { useState } from "react";
import { CounterViewModel } from "../CounterViewModel.csharp";

export const Counter = ({ model } : { model : CounterViewModel}) => {
  
  const [value, setValue] = useState(model.value)
  
  return (
      <div>
        <button onClick={() => setValue(v =>v - 1)}>-</button>
        {value}
        <button onClick={() => setValue(v =>v + 1)}>+</button>
      </div>
  )
}