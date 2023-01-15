import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import './index.css'
import {BrowserRouter} from "react-router-dom";
import {FluentProvider, teamsDarkTheme} from "@fluentui/react-components";

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <BrowserRouter>
        <FluentProvider theme={teamsDarkTheme} style={{height: "100%"}}>
            <App/>
        </FluentProvider>
    </BrowserRouter>
)
