import { Content, Grid, Column } from "@carbon/react";
import AppHeader from "./App.Header";

function App() {
    return (
        <>
            <AppHeader />
            <Content>
                <Grid>
                    <Column lg={4}>
                        <h1>Test Title</h1>
                        <h2>Test Title</h2>
                    </Column>
                </Grid>
            </Content>
        </>
    );
}

export default App;
