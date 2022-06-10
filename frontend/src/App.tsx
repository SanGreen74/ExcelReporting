import { Route, Routes } from "react-router-dom";
import "./App.css";
import Navbar from "./NavBar";
import { PkoPage } from "./pages/PkoPage";

function App(): JSX.Element {
    return (
        <>
            <Navbar />
            <div className="container">
                <Routes>
                    <Route path="/" element={<PkoPage />} />
                    <Route path="/pricing" element={<Pricing />} />
                    <Route path="/Pko" element={<PkoPage />} />
                    <Route path="/about" element={<About />} />
                </Routes>
            </div>
        </>
    );
}

function Home() {
    return <h1>Home</h1>;
}

function Pricing() {
    return <h1>Pricing</h1>;
}
function About() {
    return <h1>About</h1>;
}

export default App;
