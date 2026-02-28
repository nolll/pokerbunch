import { AxiosError } from 'axios';
import { ApiError } from './models/ApiError';

export const getErrorMessage = (error: AxiosError<ApiError>) => error.response?.data.message || 'Unknown server error';
