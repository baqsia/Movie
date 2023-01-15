import React from 'react'
import './App.css'
import {Route, Routes} from "react-router-dom";
import {MoviesList} from "./pages/movies/movies-list";
import {MoviesDetails} from "./pages/movies/movies-details";
import {NotFound} from "./pages/not-found";

export const App = () => {
    return (
        <div className="App">
            <Routes>
                <Route path="/movies" element={<MoviesList/>}/>
                <Route path="/movie/:id" element={<MoviesDetails/>}/>
                <Route path='*' element={<NotFound/>} />
            </Routes>
        </div>
    )
}

export default App
