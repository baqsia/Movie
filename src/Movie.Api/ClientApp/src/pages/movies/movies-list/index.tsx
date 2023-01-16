import {movieListStyles} from "./styles";
import {Search} from "./search";
import {fetchMovies} from "./http";
import {useEffect, useState} from "react";
import {MovieGrid} from "./movie-grid";
import * as React from "react";
import ReactPaginate from 'react-paginate';
import {MoviesListItem} from "../../../models/movie.list-item";

export const MoviesList = () => {
    const movieListStyle = movieListStyles();

    const [query, setQuery] = useState<string | null>(null);
    const [page, setPage] = useState(1);
    const [itemCount] = useState(30);
    const [movies, setMovies] = useState(Array<MoviesListItem>);
    const [pageCount, setPageCount] = useState(0);

    useEffect(() => {
        fetchMovies(query, page, itemCount).then((pagedResponse) => {
            setMovies(pagedResponse.data);
            console.log({
                totalCount: pagedResponse.totalCount,
                itemCount
            })

            setPageCount(Math.ceil(pagedResponse.totalCount / itemCount));
        });
    }, [page, query]);

    const handlePageClick = (event: { selected: number }) => {
        setPage(event.selected + 1)
    };

    return (
        <div className={movieListStyle.screen}>
            <h1>Movies List</h1>
            <Search onSearch={(val) => setQuery(val)}/>
            <MovieGrid dataSource={movies}/>
            <ReactPaginate
                pageClassName={movieListStyle.page}
                pageLinkClassName={movieListStyle.link}
                previousClassName={movieListStyle.page}
                previousLinkClassName={movieListStyle.link}
                nextClassName={movieListStyle.page}
                nextLinkClassName={movieListStyle.link}
                breakClassName={movieListStyle.page}
                breakLinkClassName={movieListStyle.link}
                containerClassName={movieListStyle.pagination}
                activeLinkClassName={movieListStyle.linkActive}
                disabledLinkClassName={movieListStyle.disabledPageBtn}
                breakLabel="..."
                nextLabel="Next"
                onPageChange={handlePageClick}
                pageRangeDisplayed={1}
                pageCount={pageCount}
                previousLabel="Previous"
            />
        </div>
    )
}
