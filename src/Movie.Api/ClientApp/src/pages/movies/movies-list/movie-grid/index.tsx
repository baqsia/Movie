import {useScrollbarWidth, useFluent} from "@fluentui/react-components";
import * as React from 'react';
import {FixedSizeList as List, ListChildComponentProps} from 'react-window';
import {
    TableBody,
    TableCell,
    TableRow,
    Table,
    TableHeader,
    TableHeaderCell,
    TableCellLayout,
    createColumn,
    useTableFeatures,
    RowState as RowStateBase
} from '@fluentui/react-components/unstable';
import {MoviesListItem} from "../../../../models/movie.list-item";
import {movieGridStyles} from "./movie-grid-styles";
import useIsMobile from "../../../../hook/isMobile";
import {useNavigate} from "react-router-dom";
import {movieYear} from "../../shared";

interface RowState extends RowStateBase<MoviesListItem> {
    onClick: (e: React.MouseEvent, item: MoviesListItem) => void;
}

interface ReactWindowRenderFnProps extends ListChildComponentProps {
    data: RowState[];
}

export interface MovieGridProps {
    dataSource: Array<MoviesListItem>
}

export const MovieGrid = (props: MovieGridProps) => {
    const navigate = useNavigate();
    const isMobile = useIsMobile();
    const styles = movieGridStyles();
    const {targetDocument} = useFluent();
    const scrollbarWidth = useScrollbarWidth({targetDocument});
    const columns = React.useMemo(() => [createColumn<MoviesListItem>({
        columnId: 'title'
    }), createColumn<MoviesListItem>({
        columnId: 'releaseDate'
    }), createColumn<MoviesListItem>({
        columnId: 'rating'
    }), createColumn<MoviesListItem>({
        columnId: 'synopsis'
    })], []);

    const items = props.dataSource;
    const {getRows} = useTableFeatures({columns, items}, []);
    const rows: RowState[] = getRows(row => {
        return {
            ...row,
            onClick: (e: React.MouseEvent, item: MoviesListItem) => {
                navigate(`/movie/${item.id}`)
            }
        };
    });

    return <div>
        <Table noNativeElements aria-label="Movies table">
            <TableHeader>
                <TableRow>
                    <TableHeaderCell key="title_key" className={styles.title}>Title </TableHeaderCell>
                    <TableHeaderCell key="release_key" className={styles.releaseDate}>Release Year</TableHeaderCell>
                    <TableHeaderCell key="rating_key" className={styles.rating}>Rating </TableHeaderCell>
                    {isMobile ? <></> : <TableHeaderCell key="synopsis_key">Synopsis</TableHeaderCell>}
                    <div role="presentation" style={{
                        width: scrollbarWidth
                    }}/>
                </TableRow>
            </TableHeader>
            <TableBody>
                <List height={600} itemCount={items.length} itemSize={60} width="100%" itemData={rows}>
                    {({index, style, data}: ReactWindowRenderFnProps) => {
                        const {item, onClick} = data[index];
                        return <TableRow aria-rowindex={index} aria-rowcount={data.length} style={style}
                                         key={index} onClick={(e) => onClick(e, item)}>
                            <TableCell className={styles.title}>
                                <TableCellLayout>{item.title}</TableCellLayout>
                            </TableCell>
                            <TableCell className={styles.releaseDate}>
                                <TableCellLayout>{movieYear(item.releaseDate)}</TableCellLayout>
                            </TableCell>
                            <TableCell className={styles.rating}>
                                <TableCellLayout>{item.rating}</TableCellLayout>
                            </TableCell>
                            {isMobile ? <></> : <TableCell>
                                <TableCellLayout>{item.synopsis}</TableCellLayout>
                            </TableCell>}
                        </TableRow>;
                    }}
                </List>
            </TableBody>
        </Table>
    </div>;
}
