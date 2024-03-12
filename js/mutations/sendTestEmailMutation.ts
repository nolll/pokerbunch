import api from '@/api';
import { MessageResponse } from '@/response/MessageResponse';
import { useMutation } from '@tanstack/vue-query';

const sendTestEmail = async (): Promise<MessageResponse> => {
  var response = await api.sendEmail();
  return response.data;
};

export const useSendTestEmailMutation = (
  onSuccess?: (response: MessageResponse) => void,
  onError?: (response: MessageResponse) => void
) => {
  return useMutation({
    mutationFn: sendTestEmail,
    onSuccess: onSuccess,
    onError: onError,
  });
};
