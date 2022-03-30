import { Registry } from './registry';

const widgets = document.querySelectorAll("[data-sfwidget]");

if (widgets && widgets.length > 0) {
  const components = [];

  widgets.forEach(widget => {
    const componentSelector = widget.getAttribute("data-sfwidget");
    const componentType = Registry[componentSelector];

    try {
      const props = {
        ...JSON.parse(widget.innerText),
        container: widget
      }
  
      if (componentType) {
        components.push({
          componentType,
          props
        });
      }

    } catch {
      console.error(`Cannot parse JSON data for component: ${componentSelector}`);
    }

    components.map(c => {
      c.componentType(c.props);
    });
  });
}