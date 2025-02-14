import { render } from '@testing-library/react';
import '@testing-library/jest-dom';
import App from '../../presentation/components/application/App';

describe("App Component Tests", () => {
    test('App renders', () => {
        render(
            <App />
        );
    });
});
