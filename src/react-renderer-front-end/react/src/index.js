import { Registry } from './registry';

const widgets = document.querySelectorAll("[data-sfwidget]");

if (widgets && widgets.length > 0) {
  const components = [];

  widgets.forEach(w => {
    const componentSelector = w.getAttribute("data-sfwidget");
    const componentType = Registry[componentSelector];

    try {
      const props = {
        ...JSON.parse(w.innerText),
        container: w
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