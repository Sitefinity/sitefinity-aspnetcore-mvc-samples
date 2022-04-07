import React from "react";
import ReactDOM from "react-dom";

const Testimonial = props => {
  ReactDOM.render(
    <div key={props.Id}>
      <p>{props.Quote}</p>
      <div>
        {props.ThumbnailUrl ? (
          <img width="50" height="50" src={props.ThumbnailUrl} alt="Test" />
        ) : null}
        <div>
          <h4>{props.Title}</h4>
          <b>{props.Company}</b>
        </div>
      </div>
    </div>,
    props.container
  )
}

export default Testimonial;