import "./css/index.scss";

import { createRoot } from "react-dom/client";
import App from "./presentation/components/application/App";

const domNode = document.getElementById("root");
const root = createRoot(domNode!);

root.render(<App />);
